using StackExchange.Redis;
namespace Infrastructure.RateLimit.Services.Interfaces
{
    public interface IInterserviceCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}
