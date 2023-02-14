using System.Net;
using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

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
            var cacheService = context.RequestServices.GetRequiredService<IInterserviceCacheService>();
            var userIpPlusRequestUrl = context.Connection.RemoteIpAddress.ToString() + context.Request.GetDisplayUrl();
            var result = await cacheService.CountRequestsFromSameUserIp(userIpPlusRequestUrl);
            if (result)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.CompleteAsync();
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
