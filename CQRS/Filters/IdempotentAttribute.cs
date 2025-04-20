﻿using CQRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CQRS.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class IdempotentAttribute : Attribute, IAsyncActionFilter
    {
        private const int DefaultCacheTimeInMinutes = 60;
        private readonly TimeSpan _cacheDuration;

        public IdempotentAttribute(int cacheTimeInMinutes = DefaultCacheTimeInMinutes)
        {
            _cacheDuration = TimeSpan.FromMinutes(cacheTimeInMinutes);
        }

        //public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            var guide = Guid.NewGuid();
            // Parse the Idempotence-Key header from the request
            if (!context.HttpContext.Request.Headers.TryGetValue(
                    "Idempotence-Key",
                    out StringValues idempotenceKeyValue) ||
                !Guid.TryParse(idempotenceKeyValue, out Guid idempotenceKey))
            {
                context.Result = new BadRequestObjectResult("Invalid or missing Idempotence-Key header");
                return;
            }

            IDistributedCache cache = context.HttpContext
                .RequestServices.GetRequiredService<IDistributedCache>();

            // Check if we already processed this request and return a cached response (if it exists)
            string cacheKey = $"Idempotent_{idempotenceKey}";
            string? cachedResult = await cache.GetStringAsync(cacheKey);
            if (cachedResult is not null)
            {
                IdempotentResponse response = JsonSerializer.Deserialize<IdempotentResponse>(cachedResult)!;

                var result = new ObjectResult(response.Value) { StatusCode = response.StatusCode };
                context.Result = result;

                return;
            }

            // Execute the request and cache the response for the specified duration
            ActionExecutedContext executedContext = await next();

            if (executedContext.Result is ObjectResult { StatusCode: >= 200 and < 300 } objectResult)
            {
                int statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;
                IdempotentResponse response = new(statusCode, objectResult.Value);

                await cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(response),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheDuration }
                );
            }
        }
    }
}
