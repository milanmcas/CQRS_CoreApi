using Polly;

namespace CQRS.CircuitBreaker
{
    public class ResilientService : IExternalService
    {
        private readonly IExternalService _externalService;
        private readonly IAsyncPolicy _policy;
        public ResilientService(IExternalService externalService, IAsyncPolicy policy)
        {
            _externalService = externalService;
            _policy = policy;
        }

        public async Task<string> GetDataAsync()
        {
            return await _policy.ExecuteAsync(() => _externalService.GetDataAsync());
        }
    }
}
