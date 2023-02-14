namespace Infrastructure.RateLimit.Services.Interfaces
{
    public interface IInterserviceCacheService
    {
        Task AddOrUpdateAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task<T> GetAsync<T>(string key);
        Task<bool> CountRequestsFromSameUserIp(string userIp);
    }
}
