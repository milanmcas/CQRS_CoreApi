namespace CQRS.Resolution
{
    public interface IService1
    {
        void DoWork();
    }
    public class Service11 : IService1
    {
        public void DoWork()
        {
            Console.WriteLine("IService1 Service11 is doing work");
        }
    }
    public class Service12 : IService1
    {
        public void DoWork()
        {
            Console.WriteLine("IService1 Service12 is doing work");
        }
    }
}
