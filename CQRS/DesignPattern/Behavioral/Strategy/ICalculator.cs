using CQRS.DesignPattern.Factory;

namespace CQRS.DesignPattern.Behavioral.Strategy
{
    public interface ICalculator
    {
        int Calculate(int value1, int value2);
    }
    public class AddCalculator : ICalculator
    {
        int ICalculator.Calculate(int value1, int value2)
        {
            return value1+value2;
        }
    }
    public class SubCalculator : ICalculator
    {
        int ICalculator.Calculate(int value1, int value2)
        {
            return value1 - value2;
        }
    }
    public class MultCalculator : ICalculator
    {
        int ICalculator.Calculate(int value1, int value2)
        {
            return value1 * value2;
        }
    }
    public class DivisionCalculator : ICalculator
    {
        int ICalculator.Calculate(int value1, int value2)
        {
            return value1 / value2;
        }
    }
    public class Calculator
    {
        public int Calculate(string type,int value1, int value2) { 
            return new CalculatorFactory().CreateCalculatorAlgo(type).Calculate(value1, value2);
        }
    }
}
