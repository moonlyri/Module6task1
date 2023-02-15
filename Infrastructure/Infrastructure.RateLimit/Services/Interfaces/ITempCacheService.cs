namespace Infrastructure.RateLimit.Services.Interfaces
{
    public interface ITempCacheService
    {
        Task AddOrUpdateAsync<T>(string key, T value);

        Task<T> GetAsync<T>(string key);
        Task<bool> CountRequestsFromSameUserIp(string userIp);
    }
}
