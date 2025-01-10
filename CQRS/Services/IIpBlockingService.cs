using System.Net;

namespace CQRS.Services
{
    public interface IIpBlockingService
    {
        bool IsBlocked(IPAddress ipAddress);
    }
    public class IpBlockingService : IIpBlockingService
    {
        private readonly List<string> _blockedIps;
        private readonly IConfiguration _configuration;
        public IpBlockingService(IConfiguration configuration)
        {
            _configuration = configuration;
            var blockedIps = configuration.GetValue<string>("BlockedIPs");
            _blockedIps = blockedIps.Split(',').ToList();
        }
        bool IIpBlockingService.IsBlocked(IPAddress ipAddress) => _blockedIps.Contains(ipAddress.ToString());
    }

}

