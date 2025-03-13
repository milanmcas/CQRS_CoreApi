namespace CQRS.CircuitBreaker
{
    public interface IExternalService
    {
        Task<string> GetDataAsync();
    }
}
