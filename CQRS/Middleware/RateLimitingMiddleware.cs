using CQRS.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace CQRS.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly RateLimitRule _rule;
        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache, RateLimitRule rule)
        {
            _next = next;
            _cache = cache;
            _rule = rule;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var identifier = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var cacheKey = $"RateLimit_{identifier}";
            var ipaddress = GetRemoteHostIpAddressUsingXForwardedFor(context);

            if (!_cache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
                _cache.Set(cacheKey, requestCount, _rule.Window);
            }

            if (requestCount >= _rule.Limit)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";

                var response = new { Message = "Rate limit exceeded. Try again later." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            _cache.Set(cacheKey, ++requestCount, _rule.Window);
            await _next(context);
        }
        private IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext httpContext)
        {
            IPAddress? remoteIpAddress = null;
            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s.Trim());
                foreach (var ip in ips)
                {
                    if (IPAddress.TryParse(ip, out var address) &&
                        (address.AddressFamily is AddressFamily.InterNetwork
                         or AddressFamily.InterNetworkV6))
                    {
                        remoteIpAddress = address;
                        break;
                    }
                }
            }
            return remoteIpAddress;
        }
    }
}
