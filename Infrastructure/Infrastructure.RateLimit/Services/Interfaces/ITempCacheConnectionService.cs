using StackExchange.Redis;
namespace Infrastructure.RateLimit.Services.Interfaces
{
    public interface ITempCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}
