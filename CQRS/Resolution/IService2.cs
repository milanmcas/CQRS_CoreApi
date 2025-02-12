namespace CQRS.Resolution
{
    public interface IService2
    {
        void DoWork();
    }
    public class Service21 : IService2
    {
        public void DoWork()
        {
            Console.WriteLine("IService2 Service21 is doing work");
        }
    }
    public class Service22 : IService2
    {
        public void DoWork()
        {
            Console.WriteLine("IService2 Service22 is doing work");
        }
    }
}
