namespace CQRS.DesignPattern.Builder
{
    public class Fruit
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
        public string Taste { get; set; }
    }
    public class FruitBuilder
    {
        private Fruit _fruit = new Fruit();

    public FruitBuilder WithName(string name)
        {
            _fruit.Name = name;
            return this;
        }

        public FruitBuilder WithColor(string color)
        {
            _fruit.Color = color;
            return this;
        }

        public FruitBuilder WithWeight(double weight)
        {
            _fruit.Weight = weight;
            return this;
        }

        public FruitBuilder WithTaste(string taste)
        {
            _fruit.Taste = taste;
            return this;
        }

        public Fruit Build()
        {
            return _fruit;
        }
    }
}
