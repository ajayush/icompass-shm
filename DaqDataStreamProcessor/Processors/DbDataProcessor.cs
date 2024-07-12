using BridgeIntelligence.CommonCore.Logger;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.EventLogger;
using BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace BridgeIntelligence.iCompass.Shm.DaqDataStreamProcessor.Api.Processors;

public class DbDataProcessor(IIxLogger logger,
        ITableStateManager tableStateManager,
        IOptions<SensorDataConfigModel> sensorDataConfig,
        IOptions<WorkerIterationConfigModel> workerIterationConfig,
        IDataEventLogger<BatchDataModel> eventSender)
    : IDataProcessor
{
    /// <summary>
    /// Method to iteratively parse data
    /// </summary>
    /// <param name="columns">Column list</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task ParseDataAsync(IList<SensorColumnMetadataModel> columns, CancellationToken cancellationToken)
    {
        if (columns.Count == 0)
        {
            return;
        }

        // Create the data structure to partition columns into queryable groups
        var groups = new Dictionary<string, List<SensorColumnMetadataModel>>();
        foreach (var column in columns)
        {
            if (!groups.ContainsKey(column.TableName))
            {
                groups.Add(column.TableName, new List<SensorColumnMetadataModel>());
            }

            groups[column.TableName].Add(column);
        }

        // Create a single sql connection
        await using var connection = new SqlConnection(_sensorDataConfig.ConnectionString);
        try
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            // Iterate over each group
            foreach (var value in groups.Values)
            {
                var lastTimeStamp = await GetInitialTimeStampAsync(connection, value[0].TableName, cancellationToken)
                    .ConfigureAwait(false);

                if (lastTimeStamp == null)
                {
                    // Nothing to parse since there are no records in the table
                    continue;
                }

                var columnsToPrint = value.Select(a => $"[{a.ColumnName}]").ToList();
                columnsToPrint.Add(_sensorDataConfig.TimestampColumnName);
                columnsToPrint.Add(_sensorDataConfig.RecordNumberColumnName);
                var selectColumns = string.Join(", ", columnsToPrint);
                var query = $"SELECT TOP {_workerIterationConfig.DefaultTake} {selectColumns} FROM [{value[0].TableName}] WHERE {_sensorDataConfig.TimestampColumnName} > @timestamp1";

                var moreResults = true;
                var cycleCount = 0;
                while (moreResults)
                {
                    // Following is to make sure no loop runs for infinite amount of time.
                    // This is to make sure, the worker if running slow, can catch up with all the tables.
                    cycleCount++;
                    if (cycleCount > _workerIterationConfig.MaxCycleCountInEachLoop)
                    {
                        break;
                    }

                    // Construct sql command for query
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@timestamp1", lastTimeStamp.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    command.CommandTimeout = 200;

                    // Execute the query
                    var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                    // Process the data
                    var parsedData = await ParseDataAsync(value, reader).ConfigureAwait(false);

                    // Check if no data was parsed
                    if (parsedData.RecNumbers.Count == 0)
                    {
                        moreResults = false;
                        continue;
                    }

                    // Send the parsed data to the event sender and save the state
                    await eventSender.SendMessageAsync(parsedData, cancellationToken).ConfigureAwait(false);

                    // Save the state
                    await tableStateManager.SaveStateAsync(parsedData, cancellationToken).ConfigureAwait(false);

                    logger.LogInformation($"Parsed data for table {value[0].TableName}. Processed {parsedData.RecNumbers.Count} points");

                    // Check if more results exist
                    if (parsedData.RecNumbers.Count == _workerIterationConfig.DefaultTake)
                    {
                        lastTimeStamp = parsedData.Timestamps.Last();
                    }
                    else
                    {
                        moreResults = false;
                    }
                }
            }
        }
        catch (Exception e)
        {
            logger.LogHandledException(e);
        }
        finally
        {
            connection.Close();
        }
    }

    private async Task<DateTime?> GetInitialTimeStampAsync(SqlConnection connection, string tableName,
        CancellationToken cancellationToken)
    {
        var lastTableState = await tableStateManager.GetAsync(tableName, cancellationToken)
            .ConfigureAwait(false);
        var lastTimeStamp = lastTableState?.LastProcessedTime;
        if (lastTimeStamp != null)
        {
            return lastTimeStamp;
        }

        lastTimeStamp = _workerIterationConfig.InitialSensorParseStartTime;

        var query = $"SELECT TOP 1 {_sensorDataConfig.TimestampColumnName} FROM [{tableName}] ORDER BY {_sensorDataConfig.TimestampColumnName} ASC";
        var command = new SqlCommand(query, connection);
        var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        if (reader.HasRows && await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            var result = (DateTime)reader[_sensorDataConfig.TimestampColumnName];
            await reader.CloseAsync().ConfigureAwait(false);

            if (lastTimeStamp > result)
            {
                return lastTimeStamp;
            }

            return result;
        }
        
        return null;
    }

    /// <summary>
    /// Method to parse sql data
    /// </summary>
    /// <param name="columns">Columns of interest</param>
    /// <param name="reader">SQL data reader</param>
    /// <returns>Parsed batch data model</returns>
    private async Task<BatchDataModel> ParseDataAsync(IList<SensorColumnMetadataModel> columns, SqlDataReader reader)
    {
        var dataModel = new BatchDataModel();
        var newColumns = columns.Where(a => a.ColumnName != _sensorDataConfig.TimestampColumnName && a.ColumnName != _sensorDataConfig.RecordNumberColumnName).ToList();

        foreach (var column in newColumns)
        {
            dataModel.DataList.Add(new BatchSingleColumnDataListModel
            {
                TableName = column.TableName,
                ColumnName = column.ColumnName,
                DataName = $"{column.TableName}.{column.ColumnName}"
            });
        }

        try
        {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                for (var i = 0; i < newColumns.Count; i++)
                {
                    dataModel.DataList[i].Values.Add(CastToFloat(reader[newColumns[i].ColumnName]));
                }

                dataModel.Timestamps.Add((DateTime)reader[_sensorDataConfig.TimestampColumnName]);
                dataModel.RecNumbers.Add((long)reader[_sensorDataConfig.RecordNumberColumnName]);
            }
        }
        finally
        {
            // Always call Close when done reading.
            await reader.CloseAsync().ConfigureAwait(false);
        }

        return dataModel;
    }

    private static float? CastToFloat(object? obj)
    {
        return obj switch
        {
            float f => f,
            _ => null
        };
    }

    private readonly SensorDataConfigModel _sensorDataConfig = sensorDataConfig.Value;
    private readonly WorkerIterationConfigModel _workerIterationConfig = workerIterationConfig.Value;
}