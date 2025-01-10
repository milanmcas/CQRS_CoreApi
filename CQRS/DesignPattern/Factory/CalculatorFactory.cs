using CQRS.DesignPattern.Behavioral.Strategy;

namespace CQRS.DesignPattern.Factory
{
    public class CalculatorFactory
    {
        public ICalculator CreateCalculatorAlgo(string type)
        {
            switch (type) {
                case "add":
                    return new AddCalculator();
                case "sub":
                    return new SubCalculator();
                case "mult":
                    return new MultCalculator();
                case "div":
                    return new DivisionCalculator();
                default:
                    return new AddCalculator();
            }
        }
    }
}
