namespace CQRS.DesignPattern.Structural.Decorator.Live
{
    /// <summary>
    /// Decorator
    /// </summary>
    public abstract class Coverage : InsurancePrice
    {
        private readonly InsurancePrice _insurancePrice;
        public Coverage(InsurancePrice insurancePrice)
        {
            _insurancePrice = insurancePrice;
        }
    }
    /// <summary>
    /// Concrete decorator CarCivilLiability
    /// </summary>
    public class CarCivilLiability : Coverage
    {
        private readonly InsurancePrice _insurancePrice;
        public CarCivilLiability(InsurancePrice insurancePrice) : base(insurancePrice)
        {
            {
                _insurancePrice = insurancePrice;
            }
        }

        public override double BasePrice()
        {
            return _insurancePrice.BasePrice()+ this.BasicPrice;
        }

        public override double Discount()
        {
            return _insurancePrice.Discount()+this.BaseDiscount;
        }

        public override double Price()
        {
            return _insurancePrice.Price()+this.NetPrice;
        }
    }
    /// <summary>
    /// Concrete decorator FireAndTheft
    /// </summary>
    public class FireAndTheft : Coverage
    {
        private readonly InsurancePrice _insurancePrice;
        public FireAndTheft(InsurancePrice insurancePrice) : base(insurancePrice)
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
