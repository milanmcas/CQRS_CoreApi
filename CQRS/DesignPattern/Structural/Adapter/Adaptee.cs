namespace CQRS.DesignPattern.Structural.Adapter
{
    public interface IAnalyticsService
    {
        void GenerateReport(string json);
    }
    public class AnalyticsService : IAnalyticsService
    {
        public void GenerateReport(string json)
        {
            Console.WriteLine(json);
        }
    }
}
