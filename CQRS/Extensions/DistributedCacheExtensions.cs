using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace CQRS.Extensions
{
    public static class DistributedCacheExtensions
    {
        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        {
            return SetAsync(cache, key, value, new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
        }
        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, serializerOptions));
            return cache.SetAsync(key, bytes, options);
        }
        public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? value)
        {
            var val = cache.Get(key);
            value = default;
            if (val == null) return false;
            value = JsonSerializer.Deserialize<T>(val, serializerOptions);
            return true;
        }

        public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> task, DistributedCacheEntryOptions? options = null)
        {
            if (options == null)
            {
                options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))//Sliding expiration expires the cacheitem if it has not been accessed within the timespan provided. 
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));//Absolute expiration expires the cacheitem when the given time has been reached.
                
            }
            if (cache.TryGetValue(key, out T? value) && value is not null)
            {
                return value;
            }
            value = await task();
            if (value is not null)
            {
                await cache.SetAsync<T>(key, value, options);
            }
            return value;
        }
    }
}
