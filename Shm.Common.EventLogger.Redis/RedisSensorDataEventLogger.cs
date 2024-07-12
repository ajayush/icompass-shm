using BridgeIntelligence.iCompass.Shm.Common.ApiModels.TableStates;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BridgeIntelligence.iCompass.Shm.Common.EventLogger.Redis;

public class RedisSensorDataEventLogger(ConnectionMultiplexer redis, IOptions<RedisConfig> redisConfig) : RedisDataEventLogger<BatchDataModel>(redis, redisConfig.Value.SensorChannelName);