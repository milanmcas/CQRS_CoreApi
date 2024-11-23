namespace CQRS.DesignPattern.Structural.Decorator
{
    public abstract class FoodDecorator:Food
    {
        private Food _food;
        public FoodDecorator(Food food) {
            _food = food;
        }
    }
    public class CheeseDecorator : FoodDecorator
    {
        private Food _food;
        public CheeseDecorator(Food food) : base(food) 
        {
            _food = food;
        }

        public override double Cost()
        {
            return _food.Cost()+2.0;
        }

        public override string Description()
        {
            return _food.Description()+" Cheese added.";
        }
    }
}
