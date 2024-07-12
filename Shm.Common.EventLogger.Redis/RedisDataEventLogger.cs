using Newtonsoft.Json;
using StackExchange.Redis;

namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger.Redis;

public abstract class RedisDataEventLogger<T>(ConnectionMultiplexer redis, string channelName) : IDataEventLogger<T>, IAsyncDisposable
{
    public async Task SendMessageAsync(T output, CancellationToken cancellationToken)
    {
        await redis.GetSubscriber().PublishAsync(channelName, new RedisValue(JsonConvert.SerializeObject(output)));
    }

    public void ReceiveMessages(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken)
    {
        async void Handler(ChannelMessage message)
        {
            var convertedMessage = JsonConvert.DeserializeObject<T>(message.Message!)!;
            await handler(convertedMessage, cancellationToken);
        }

        redis.GetSubscriber().Subscribe(channelName).OnMessage(Handler);
    }

    public async ValueTask DisposeAsync()
    {
        await redis.DisposeAsync().ConfigureAwait(false);
    }
}