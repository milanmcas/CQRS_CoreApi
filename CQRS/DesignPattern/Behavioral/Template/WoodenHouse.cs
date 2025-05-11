namespace CQRS.DesignPattern.Behavioral.Template
{
    public class WoodenHouse : HouseTemplate
    {
        protected sealed override void BuildFoundation()
        {
            Console.WriteLine("Building foundation with cement, iron rods, wood and sand");
        }
        protected sealed override void BuildPillars()
        {
            Console.WriteLine("Building wood Pillars with wood coating");
        }
        protected sealed override void BuildWalls()
        {
            Console.WriteLine("Building Wood Walls");
        }
        protected sealed override void BuildWindows()
        {
            Console.WriteLine("Building Wood Windows");
        }
    }
}
