namespace Infrastructure.RateLimit.Config
{
    public class RedisConfig
    {
        public string Host { get; set; } = null!;
        public TimeSpan CacheTimeout { get; set; }
        public TimeSpan RateLimitCacheTimeout { get; set; }
    }
}
