using static CQRS.Services.SingletonService;

namespace CQRS.Services
{
    public interface IScopedService
    {
        void Print();
    }
    public class ScopedService : IScopedService
    {
        public Guid a = Guid.Empty;
        private readonly ITransientService _transientService ;
        public ScopedService(

            ITransientService transientService
            ) {
            a = Guid.NewGuid();
            _transientService = transientService ;
        }
        void IScopedService.Print()
        {
            
            Console.WriteLine($"IScopedService.Print - {a}");
            _transientService.Print();
        }
    }
    public interface IScopedService1
    {
        void Print();
    }
    public class ScopedService1 : IScopedService1
    {
        public Guid a = Guid.Empty;
        
        public ScopedService1()
        {
            a = Guid.NewGuid();            
        }
        void IScopedService1.Print()
        {

            Console.WriteLine($"IScopedService1.Print - {a}");
            
        }
    }
    public interface IScopedService2
    {
        void Print();
    }
    public class ScopedService2 : IScopedService2
    {
        public Guid a = Guid.Empty;
        private readonly ISingletonService1 _singletonService1;
        public ScopedService2(ISingletonService1 singletonService1)
        {
            a = Guid.NewGuid();
            _singletonService1 = singletonService1 ;
        }
        void IScopedService2.Print()
        {
            _singletonService1.Print();
            Console.WriteLine($"IScopedService1.Print - {a}");

        }
    }
}
