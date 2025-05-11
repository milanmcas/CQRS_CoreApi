namespace CQRS.DesignPattern.Behavioral.Template
{
    public class ConcreteHouse : HouseTemplate
    {
        protected sealed override void BuildFoundation()
        {
            Console.WriteLine("Building foundation with cement, iron rods and sand");
        }
        protected sealed override void BuildPillars()
        {
            Console.WriteLine("Building Concrete Pillars with Cement and Sand");
        }
        protected sealed override void BuildWalls()
        {
            Console.WriteLine("Building Concrete Walls");
        }
        protected sealed override void BuildWindows()
        {
            Console.WriteLine("Building Concrete Windows");
        }
    }
}
