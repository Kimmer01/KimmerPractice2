using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplicationWebAPI.Filters
{
    public class RateLimitFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;

        public RateLimitFilter(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();

            string cacheKey = $"LastVisitTick_{ip}";
            long? lastTick = _cache.Get<long?>(cacheKey);
            if (lastTick == null || Environment.TickCount64 - lastTick > 1000)
            {
                _cache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));//缓存10秒过期
                return next();
            }

            context.Result = new ContentResult { StatusCode = 429 };//429代表访问过于频繁
            return Task.CompletedTask;
        }
    }
}
