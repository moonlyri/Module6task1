using Infrastructure.RateLimit.Services.Interfaces;
using Infrastructure.RateLimit.Config;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.RateLimit.Services
{
    public class TempCacheConnectionService : ITempCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public TempCacheConnectionService(IOptions<RedisConfig> config)
        {
            var redisConfigurationOptions = ConfigurationOptions.Parse(config.Value.Host);
            _connectionLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConfigurationOptions));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}
