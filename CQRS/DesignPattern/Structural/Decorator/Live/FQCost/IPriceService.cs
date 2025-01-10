namespace CQRS.DesignPattern.Structural.Decorator.Live.FQCost
{
    /// <summary>
    /// Component Class
    /// </summary>
    public interface IPriceService
    {
        double BasePrice();
        double Price();
        double Discount();
    }
    /// <summary>
    /// Concrete Component
    /// </summary>
    public class ClassicPriceService : IPriceService
    {
        public double BasePrice()
        {
            return 1000;
        }

        public double Discount()
        {
            return 10;
        }

        public double Price()
        {
            return 990;
        }
    }
    public class TelematicsPriceService : IPriceService
    {
        private readonly IPriceService _priceService;
        public TelematicsPriceService(IPriceService priceService)
        {
            _priceService= priceService;
        }
        public double BasePrice()
        {
            return _priceService.BasePrice()+ 100;
        }

        public double Discount()
        {
            return _priceService.Discount() + 1;
        }

        public double Price()
        {
            return _priceService.Price()+ 990;
        }
    }
}
