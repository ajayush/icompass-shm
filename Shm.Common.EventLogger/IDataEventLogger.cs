namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger;

public interface IDataEventLogger<T>
{
    Task SendMessageAsync(T output, CancellationToken cancellationToken);

    void ReceiveMessages(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken);
}