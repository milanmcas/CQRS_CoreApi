namespace CQRS.DesignPattern.DisposePattern
{
    public class DisposePatternTest
    {
        public static void MainMethod()
        {
            using(MyResource resourceDerived=new ResourceDerived())
            {
                Console.WriteLine("DisposePatternTest test");
            };
            using (ResourceDerived resourceDerived = new ResourceDerived())
            {
                Console.WriteLine("DisposePatternTest test");
            }
            ;
        }
    }
}
