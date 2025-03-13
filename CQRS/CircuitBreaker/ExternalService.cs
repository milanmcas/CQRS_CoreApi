
namespace CQRS.CircuitBreaker
{
    public class ExternalService : IExternalService
    {
        async Task<string> IExternalService.GetDataAsync()
        {
            try
            {
                Console.WriteLine("This is the external service");
                int a = 10, b = 0;
                int c = a / b;
                await Task.Delay(10);
                return "This is the external service";
            }
            catch(Exception)
            {
                throw;
            }
            
        }
    }
}
