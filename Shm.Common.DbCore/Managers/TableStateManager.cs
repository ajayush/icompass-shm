using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class TableStateManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : ITableStateManager
{
    /// <summary>
    /// Method to get table state, given its id
    /// </summary>
    /// <param name="tableName">Table Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An instance of <see cref="TableStateReturnModel"/></returns>
    public async Task<TableStateReturnModel?> GetAsync(string tableName, CancellationToken cancellationToken)
    {
        var state = await dbContext.TableStates.FirstOrDefaultAsync(a => a.TableName == tableName, cancellationToken)
            .ConfigureAwait(false);

        return state == null ? null : ToReturnModel(state);
    }

    /// <summary>
    /// Method to save the state for next iteration
    /// </summary>
    /// <param name="model">Parsed data model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task SaveStateAsync(BatchDataModel model, CancellationToken cancellationToken)
    {
        if (model.RecNumbers.Count == 0 || model.DataList.Count == 0)
        {
            return;
        }

        var tableStates = await dbContext.TableStates.ToListAsync(cancellationToken).ConfigureAwait(false);
        var tableState = tableStates.FirstOrDefault(a => a.TableName == model.DataList.First().TableName);
        if (tableState == null)
        {
            tableState = new TableState
            {
                TableName = model.DataList.First().TableName,
                LastProcessedTime = model.Timestamps.Last(),
                LastRecNum = model.RecNumbers.Last()
            };
            dbContext.TableStates.Add(tableState);
        }

        tableState.LastRecNum = model.RecNumbers.Last();
        tableState.LastProcessedTime = model.Timestamps.Last();
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private static TableStateReturnModel ToReturnModel(TableState model)
    {
        return new TableStateReturnModel
        {
            TableName = model.TableName,
            LastRecNum = model.LastRecNum,
            LastProcessedTime = model.LastProcessedTime
        };
    }
}