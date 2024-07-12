using BridgeIntelligence.CommonCore.EventPipeline.Models;

namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger;

public class NullShmDataEventLogger : IDataEventLogger<EventLogModel>
{
    public Task SendMessageAsync(EventLogModel output, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void ReceiveMessages(Func<EventLogModel, CancellationToken, Task> handler, CancellationToken cancellationToken)
    {
    }
}