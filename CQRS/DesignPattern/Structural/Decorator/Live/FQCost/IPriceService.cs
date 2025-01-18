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
        private double _basePrice=1000;
        private double _discount=10;
        private double _price=1010;
        public double BasePrice()
        {
            return _basePrice;            
        }

        public double Discount()
        {
            return _discount;
        }

        public double Price()
        {
            return _price;
        }
    }
    public class TelematicsPriceService : IPriceService
    {
        private double _basePrice = 1000;
        private double _discount = 1;
        private double _price = 1010;
        private readonly IPriceService _priceService;
        public TelematicsPriceService(IPriceService priceService)
        {
            _priceService= priceService;
        }
        public double BasePrice()
        {
            return _priceService.BasePrice()+ _basePrice;
        }

        public double Discount()
        {
            return _priceService.Discount() + _discount;
        }

        public double Price()
        {
            return _priceService.Price()+ _price;
        }
    }
    public abstract class PriceService : IPriceService
    {
        protected double _basePrice = 1000;
        protected double _discount = 1;
        protected double _price = 1010;
        private readonly IPriceService _priceService;
        public PriceService(IPriceService priceService)
        {
            _priceService = priceService;
        }

        public abstract double BasePrice();
        public abstract double Discount();
        public abstract double Price();
    }
    public class TelematicsPriceCalculationService : PriceService
    {
       
        private readonly IPriceService _priceService;
        public TelematicsPriceCalculationService(IPriceService priceService):base(priceService) 
        {
            _priceService = priceService;
        }
        public override double BasePrice()
        {
            return _priceService.BasePrice() + _basePrice;
        }

        public override double Discount()
        {
            return _priceService.Discount() + _discount;
        }

        public override double Price()
        {
            return _priceService.Price() + _price;
        }
    }
}
