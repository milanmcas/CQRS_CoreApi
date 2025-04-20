namespace CQRS.Models
{
    public class CacheConfiguration
    {
        public int AbsoluteExpirationInHours { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
    public enum CacheTech
    {
        Redis,
        Memory
    }
}
