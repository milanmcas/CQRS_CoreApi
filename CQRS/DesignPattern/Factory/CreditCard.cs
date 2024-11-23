namespace CQRS.DesignPattern.Factory
{
    public abstract class CreditCard
    {
        public abstract string GetCardType();
        public abstract int GetCreditLimit();
        public abstract int GetAnnualCharge();
    }
    public class MoneyBack : CreditCard
    {
        public override string GetCardType()
        {
            return "MoneyBack";
        }
        public override int GetCreditLimit()
        {
            return 15000;
        }
        public override int GetAnnualCharge()
        {
            return 500;
        }
    }
    public class Titanium : CreditCard
    {
        public override string GetCardType()
        {
            return "Titanium Edge";
        }
        public override int GetCreditLimit()
        {
            return 25000;
        }
        public override int GetAnnualCharge()
        {
            return 1500;
        }
    }
    public class Platinum : CreditCard
    {
        public override string GetCardType()
        {
            return "Platinum Plus";
        }
        public override int GetCreditLimit()
        {
            return 35000;
        }
        public override int GetAnnualCharge()
        {
            return 2000;
        }
    }
}
