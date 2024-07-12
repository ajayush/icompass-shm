using BridgeIntelligence.CommonCore.EventPipeline.Models;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger.Redis;

public class RedisShmDataEventLogger(ConnectionMultiplexer redis, IOptions<RedisConfig> redisConfig) : RedisDataEventLogger<EventLogModel>(redis, redisConfig.Value.ShmChannelName);