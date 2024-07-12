using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;

namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger;

public class NullSensorDataEventLogger : IDataEventLogger<BatchDataModel>
{
    public Task SendMessageAsync(BatchDataModel output, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void ReceiveMessages(Func<BatchDataModel, CancellationToken, Task> handler, CancellationToken cancellationToken)
    {
    }
}