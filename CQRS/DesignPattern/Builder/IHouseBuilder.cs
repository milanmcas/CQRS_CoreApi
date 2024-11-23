namespace CQRS.DesignPattern.Builder
{
    public interface IHouseBuilder
    {
        IHouseBuilder WithWindows(int number);
        IHouseBuilder WithDoors(int number);
        IHouseBuilder WithGarden(bool hasGarden);

        House Build();
    }
    public class HouseBuilder : IHouseBuilder
    {
        private readonly House _house;
        public HouseBuilder() {
            _house=new House();
        }
        House IHouseBuilder.Build()
        {
            return _house;
        }

        IHouseBuilder IHouseBuilder.WithDoors(int number)
        {
            _house.NumberOfDoors=number;
            return this;// Return the builder for chaining
        }

        IHouseBuilder IHouseBuilder.WithGarden(bool hasGarden)
        {
            _house.HasGarden=hasGarden;
            return this;// Return the builder for chaining
        }

        IHouseBuilder IHouseBuilder.WithWindows(int number)
        {
            _house.NumberOfWindows=number;
            return this;// Return the builder for chaining
        }
    }
}
