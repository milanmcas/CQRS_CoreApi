namespace CQRS.Resolution
{
    public delegate IService ServiceResolver(string key);
    public interface IService
    {
        void DoWork();
    }
    public class Service1 : IService
    {
        public void DoWork()
        {
            Console.WriteLine("IService Service1 is doing work");
        }
    }
    public class Service2 : IService
    {
        public void DoWork()
        {
            Console.WriteLine("IService Service2 is doing work");
        }
    }
}
