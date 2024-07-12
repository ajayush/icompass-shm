using System.Data.Common;
using BridgeIntelligence.CommonCore.Utilities;
using BridgeIntelligence.iCompass.Shm.Common.DbCore;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.SeedMigrator.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BridgeIntelligence.iCompass.Shm.Common.SeedMigrator;

internal class SeedMigrator
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var fileName = Path.Combine(PathExtensions.GetDirectoryPathForBin("Config"), "SensorDetails_20240227.json");
        var contents = await File.ReadAllTextAsync(fileName, cancellationToken);
        var seedJsonModel = JsonConvert.DeserializeObject<SeedJsonModel>(contents)!;
        var conn = new DbConnectionStringBuilder().ConnectionString = "Server=J-Desktop;Initial Catalog=icompass_shm_4;MultipleActiveResultSets=True;Connection Timeout=30;Integrated Security=True;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<ShmDbContext>().UseSqlServer(conn);
        var dbContext = new ShmDbContext(optionsBuilder.Options);

        foreach (var bridge in seedJsonModel.Bridges)
        {
            var bridgeEntity = new Bridge
            {
                BridgeAbbreviation = bridge.BridgeAbbreviation,
                BridgeName = bridge.BridgeName
            };

            dbContext.Bridges.Add(bridgeEntity);
        }

        Console.WriteLine("Saving bridges");
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        var bridgesCreated = dbContext.Bridges.ToDictionary(d => d.BridgeAbbreviation, d => d);

        foreach (var daq in seedJsonModel.Daqs)
        {
            var bridge = bridgesCreated[daq.BridgeName];
            var daqEntity = new Daq
            {
                BridgeId = bridge.Id,
                Name = daq.Name,
                Model = daq.Model,
                SpanOrPier = daq.SpanOrPier,
                UpstreamDownstream = daq.UpstreamDownstream,
                Location = daq.Location,
                IsActive = true,
                DateCreated = DateTimeOffset.Now,
                DateLastModified = DateTimeOffset.Now
            };

            dbContext.Daqs.Add(daqEntity);
        }

        Console.WriteLine("Saving daqs");
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        var daqsCreated = dbContext.Daqs.ToDictionary(d => $"{d.BridgeId}:{d.Name}", d => d);

        foreach (var sensor in seedJsonModel.Sensors)
        {
            var bridge = bridgesCreated[sensor.BridgeName];
            var daq = daqsCreated[$"{bridge.Id}:{sensor.DaqName}"];
            var sensorEntity = new Sensor
            {
                DaqId = daq.Id,
                Name = sensor.Name,
                Abbreviation = sensor.Abbreviation,
                AlternateName = sensor.AlternateName,
                System = sensor.System,
                Model = sensor.Model,
                SpanOrPier = sensor.SpanOrPier,
                UpstreamDownstream = sensor.UpstreamDownstream,
                Member = sensor.Member,
                Position = sensor.Position,
                IsActive = true,
                DateCreated = DateTimeOffset.Now,
                DateLastModified = DateTimeOffset.Now
            };

            dbContext.Sensors.Add(sensorEntity);
        }

        Console.WriteLine("Saving sensors");
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        var sensorsCreated = dbContext.Sensors.ToDictionary(s => s.Name, s => s);

        foreach (var sensorOutput in seedJsonModel.SensorOutputs)
        {
            if (!sensorsCreated.ContainsKey(sensorOutput.SensorName))
            {
                Console.WriteLine($"Missing {sensorOutput.SensorName}");
                continue;
            }
            var sensor = sensorsCreated[sensorOutput.SensorName];
            var sensorOutputEntity = new SensorOutput
            {
                SensorId = sensor.Id,
                OutputType = sensorOutput.OutputType,
                TableName = sensorOutput.TableName,
                ColumnName = sensorOutput.ColumnName,
                SamplingFrequency = sensorOutput.SamplingFrequency,
                Unit = sensorOutput.Unit,
                UpperBound = sensorOutput.UpperBound,
                LowerBound = sensorOutput.LowerBound,
                IsActive = true,
                DateCreated = DateTimeOffset.Now,
                DateLastModified = DateTimeOffset.Now
            };

            dbContext.SensorOutputs.Add(sensorOutputEntity);
        }

        Console.WriteLine("Saving sensor outputs");
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task ValidateAsync(CancellationToken cancellationToken)
    {
        var fileName = Path.Combine(PathExtensions.GetDirectoryPathForBin("Config"), "allTablesColumnsSensors.txt");
        var contents = await File.ReadAllLinesAsync(fileName, cancellationToken).ConfigureAwait(false);
        var columnSet = contents.ToHashSet();
        var conn = new DbConnectionStringBuilder().ConnectionString = "Server=J-Desktop;Initial Catalog=icompass_shm_4;MultipleActiveResultSets=True;Connection Timeout=30;Integrated Security=True;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<ShmDbContext>().UseSqlServer(conn);
        var dbContext = new ShmDbContext(optionsBuilder.Options);

        foreach (var output in dbContext.SensorOutputs.OrderBy(a=>a.TableName))
        {
            var columnName = $"{output.TableName}.{output.ColumnName}";
            if (!columnSet.Contains(columnName))
            {
                Console.WriteLine($"Missing {columnName}");
            }
        }
    }
}