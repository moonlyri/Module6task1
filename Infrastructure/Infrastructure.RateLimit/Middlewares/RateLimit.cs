using System.Net;
using Infrastructure.RateLimit.Services.Interfaces;

namespace Infrastructure.RateLimit.Middlewares
{
    public class RateLimit
    {
        private readonly RequestDelegate _next;

        public RateLimit(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cacheService = context.RequestServices.GetRequiredService<ITempCacheService>();
            var userIp = context.Request.Host.Host;
            var result = await cacheService.CountRequestsFromSameUserIp(userIp);
            if (result)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            }
            await _next(context);
        }
    }

    public static class RateLimitExtension
    {
        public static IApplicationBuilder UseRateLimit(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimit>();
        }
    }
}
