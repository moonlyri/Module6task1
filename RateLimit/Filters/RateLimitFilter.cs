using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Infrastructure.RateLimit.Filters
{
    public class RateLimitFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IInterserviceCacheService>();
            var userIpPlusRequestUrl = context.HttpContext.Connection.RemoteIpAddress.ToString() + context.HttpContext.Request.GetDisplayUrl();
            var result = await cacheService.CountRequestsFromSameUserIp(userIpPlusRequestUrl);
            if (result)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.HttpContext.Response.CompleteAsync();
            }
            await next();
        }
    }
}
