using CQRS.DesignPattern.Prototype;

namespace CQRS.DesignPattern.Structural.Adapter
{
    public interface IAnalyticsAdapter
    {
        void ProcessEmployees(List<Customer> employees);
    }
    public class AnalyticsAdapter : IAnalyticsAdapter
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsAdapter(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public void ProcessEmployees(List<Customer> employees)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(employees);

            _analyticsService.GenerateReport(json);
        }
    }
}
