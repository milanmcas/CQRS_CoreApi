namespace CQRS.DesignPattern.Structural.Decorator
{
    public class Sandwich : Food
    {
        public override double Cost()
        {
            return 1.0;
        }

        public override string Description()
        {
            return "Sandwich";
        }
    }
}
