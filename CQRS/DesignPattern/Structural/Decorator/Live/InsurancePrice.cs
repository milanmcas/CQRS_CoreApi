namespace CQRS.DesignPattern.Structural.Decorator.Live
{
    /// <summary>
    /// Component Class
    /// </summary>
    public abstract class InsurancePrice
    {
        public double BasicPrice {  get; set; }
        public double NetPrice { get; set; }
        public double BaseDiscount { get; set; }
        public abstract double BasePrice();
        public abstract double Price();
        public abstract double Discount();
    }
    /// <summary>
    /// Concrete Component Class
    /// </summary>
    public class ClassicPrice : InsurancePrice
    {
        public override double BasePrice()
        {
            return BasicPrice;
        }

        public override double Discount()
        {
            return NetPrice;
        }

        public override double Price()
        {
            return BaseDiscount;
        }
    }
    /// <summary>
    /// Concrete Component Class
    /// </summary>
    public class TelematicsPrice : InsurancePrice
    {
        public override double BasePrice()
        {
            return BasicPrice;
        }

        public override double Discount()
        {
            return NetPrice;
        }

        public override double Price()
        {
            return BaseDiscount;
        }
    }
}
