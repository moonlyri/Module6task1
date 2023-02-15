using Microsoft.Extensions.Options;
using Infrastructure.RateLimit.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.RateLimit.Config;
using StackExchange.Redis;

namespace Infrastructure.RateLimit.Services
{
    public class TempCacheService : ITempCacheService
    {
        private readonly ILogger<TempCacheService> _logger;
        private readonly ITempCacheConnectionService _redisCacheConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RedisConfig _config;
        private readonly IHttpContextAccessor _context;

        public TempCacheService(
            ILogger<TempCacheService> logger,
            ITempCacheConnectionService redisCacheConnectionService,
            IOptions<RedisConfig> config,
            IJsonSerializer jsonSerializer,
            IHttpContextAccessor context)
        {
            _logger = logger;
            _redisCacheConnectionService = redisCacheConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;
            _context = context;
        }

        public Task AddOrUpdateAsync<T>(string key, T value)
        => AddOrUpdateInternalAsync(key, value);

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);

            return serialized.HasValue ?
                _jsonSerializer.Deserialize<T>(serialized.ToString())
                : default(T)!;
        }

        private string GetItemCacheKey(string userId) =>
            $"{userId}";

        private async Task AddOrUpdateInternalAsync<T>(string key, T value,
            IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;
            var cacheKey = GetItemCacheKey(key);
            var serialized = _jsonSerializer.Serialize(value);

            if (await redis.StringSetAsync(cacheKey, serialized, expiry))
            {
                _logger.LogInformation($"Cached value for key {key} cached");
            }
            else
            {
                _logger.LogInformation($"Cached value for key {key} updated");
            }
        }

        public async Task<bool> CountRequestsFromSameUserIp(string userIp)
        {
            var result = await GetAsync<ConutConfig>(userIp);
            if (result == null)
            {
                await AddOrUpdateAsync<ConutConfig>(userIp, new ConutConfig() { Count = 1 });
                return false;
            }
            else if (result.Count >= 10)
            {
                return true;
            }

            await AddOrUpdateAsync<ConutConfig>(userIp, new ConutConfig() { Count = result.Count + 1 });
            return false;
        }

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
    }
}
