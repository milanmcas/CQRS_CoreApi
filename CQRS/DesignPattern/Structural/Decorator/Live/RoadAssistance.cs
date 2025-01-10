namespace CQRS.DesignPattern.Structural.Decorator.Live
{
    /// <summary>
    /// Concrete decorator RoadAssistance
    /// </summary>
    public class RoadAssistance : Coverage
    {
        private readonly InsurancePrice _insurancePrice;
        public RoadAssistance(InsurancePrice insurancePrice) : base(insurancePrice)
        {
            {
                _insurancePrice = insurancePrice;
            }
        }
        public override double BasePrice()
        {
            return _insurancePrice.BasePrice() + this.BasicPrice;
        }

        public override double Discount()
        {
            return _insurancePrice.Discount() + this.BaseDiscount;
        }

        public override double Price()
        {
            return _insurancePrice.Price() + this.NetPrice;
        }
    }
}
