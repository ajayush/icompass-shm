using BridgeIntelligence.CommonCore.Models;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.Health;
using BridgeIntelligence.iCompass.Shm.Common.ApiModels.WatchdogQuery;
using BridgeIntelligence.iCompass.Shm.Common.DbInterfaces;
using BridgeIntelligence.iCompass.Shm.Common.DbModels;
using BridgeIntelligence.iCompass.Shm.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BridgeIntelligence.iCompass.Shm.Common.DbCore.Managers;

public class WatchdogQueryManager(PerRequestContext perRequestContext, ShmDbContext dbContext) : IWatchdogQueryManager
{
    public async Task<CommonListModel<DaqGridReturnModel>> QueryAsync(WatchdogDaqQueryModel model,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var query = GetQuery(model);

        var results = await query.Select(a => new DaqGridReturnModel
        {
            UpstreamDownstream = a.UpstreamDownstream,
            BridgeId = a.BridgeId,
            BridgeName = a.Bridge.BridgeName,
            BridgeAbbreviation = a.Bridge.BridgeAbbreviation,
            DaqId = a.Id,
            Model = a.Model,
            DaqName = a.Name,
            IsHealthy = !a.IsFailedState,
            Location = a.Location,
            SpanOrPier = a.SpanOrPier,
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        if (model.Status.HasValue)
        {
            if (model.Status.Value == HealthStatus.Error)
            {
                results = results.Where(a => a.IsHealthy == false).ToList();
            }
            else if (model.Status.Value == HealthStatus.Success)
            {
                results = results.Where(a => a.IsHealthy == true).ToList();
            }
        }

        return new CommonListModel<DaqGridReturnModel>
        {
            Count = results.Count,
            TotalCount = results.Count,
            Items = results
        };
    }

    public async Task<CommonListModel<SensorGridReturnModel>> QueryAsync(WatchdogSensorQueryModel model,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var query = GetQuery(model);

        var results = await query.Select(a => new SensorGridReturnModel
        {
            UpstreamDownstream = a.Sensor.UpstreamDownstream,
            System = a.Sensor.System,
            BridgeId = a.Sensor.Daq.BridgeId,
            BridgeName = a.Sensor.Daq.Bridge.BridgeName,
            SensorId = a.Id,
            Model = a.Sensor.Model,
            SensorName = a.Sensor.Name,
            SpanOrPier = a.Sensor.SpanOrPier,
            Member = a.Sensor.Member,
            Position = a.Sensor.Position,
            DaqName = a.Sensor.Daq.Name,
            IsHealthy = !a.Sensor.SensorOutputs.Any(b => b.AlertRules.Any(c => c.IsFailedState && !c.IsDeleted)),
            DaqId = a.Sensor.DaqId,
            SensorOutputId = a.Id,
            SensorOutputType = a.OutputType

        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        if (model.Status.HasValue)
        {
            if (model.Status.Value == HealthStatus.Error)
            {
                results = results.Where(a => !a.IsHealthy).ToList();
            }
            else if (model.Status.Value == HealthStatus.Success)
            {
                results = results.Where(a => a.IsHealthy).ToList();
            }
        }

        return new CommonListModel<SensorGridReturnModel>
        {
            Count = results.Count,
            TotalCount = results.Count,
            Items = results
        };
    }

    public async Task<CommonListModel<AlertGridReturnModel>> QueryAsync(WatchdogAlertQueryModel model,
        CancellationToken cancellationToken)
    {
        perRequestContext.Logger.LogTrace();
        var query = GetQuery(model);

        var results = await query.Select(a => new AlertGridReturnModel
        {
            DaqId = a.SensorOutput.Sensor.DaqId,
            Action = "",
            SensorId = a.SensorOutput.SensorId,
            BridgeId = a.SensorOutput.Sensor.Daq.BridgeId,
            RuleName = a.RuleName,
            AlertType = a.AlertType,
            AlertRuleId = a.Id,
            AlertTime = a.DateCreated,
            BridgeName = a.SensorOutput.Sensor.Daq.Bridge.BridgeName,
            DaqName = a.SensorOutput.Sensor.Daq.Name,
            SensorName = a.SensorOutput.Sensor.Name
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        return new CommonListModel<AlertGridReturnModel>
        {
            Count = results.Count,
            TotalCount = results.Count,
            Items = results
        };
    }

    private IQueryable<AlertRule> GetQuery(WatchdogAlertQueryModel model)
    {
        var query = dbContext.AlertRules.Where(a => !a.IsDeleted);

        if (model.DaqId.HasValue)
        {
            query = query.Where(a => a.SensorOutput.Sensor.DaqId == model.DaqId.Value);
        }

        if (model.SensorId.HasValue)
        {
            query = query.Where(a => a.SensorOutput.SensorId == model.SensorId.Value);
        }

        if (!string.IsNullOrEmpty(model.System))
        {
            query = query.Where(a => a.SensorOutput.Sensor.System == model.System);
        }

        if (model.BridgeId.HasValue)
        {
            query = query.Where(a => a.SensorOutput.Sensor.Daq.BridgeId == model.BridgeId.Value);
        }

        if (model.DateFrom.HasValue)
        {
            query = query.Where(a => a.DateCreated >= model.DateFrom.Value);
        }

        if (model.DateTo.HasValue)
        {
            query = query.Where(a => a.DateCreated <= model.DateTo.Value);
        }

        return query;
    }

    private IQueryable<Daq> GetQuery(WatchdogDaqQueryModel model)
    {
        var query = dbContext.Daqs.Where(a => a.IsActive && !a.IsDeleted);

        if (model.BridgeId.HasValue)
        {
            query = query.Where(a => a.BridgeId == model.BridgeId.Value);
        }

        if (!string.IsNullOrEmpty(model.UpstreamDownstream))
        {
            query = query.Where(a => a.UpstreamDownstream == model.UpstreamDownstream);
        }

        return query;
    }

    private IQueryable<SensorOutput> GetQuery(WatchdogSensorQueryModel model)
    {
        var query = dbContext.SensorOutputs.Where(a =>
            a.Sensor.IsActive && !a.Sensor.IsDeleted &&
            a.IsActive && !a.IsDeleted &&
            a.Sensor.Daq.IsActive && !a.Sensor.Daq.IsDeleted);

        if (model.BridgeId.HasValue)
        {
            query = query.Where(a => a.Sensor.Daq.BridgeId == model.BridgeId.Value);
        }

        if (!string.IsNullOrEmpty(model.System))
        {
            query = query.Where(a => a.Sensor.System == model.System);
        }

        if (!string.IsNullOrEmpty(model.UpstreamDownstream))
        {
            query = query.Where(a => a.Sensor.UpstreamDownstream == model.UpstreamDownstream);
        }

        if (!string.IsNullOrEmpty(model.SpanOrPier))
        {
            query = query.Where(a => a.Sensor.SpanOrPier == model.SpanOrPier);
        }

        return query;
    }
}