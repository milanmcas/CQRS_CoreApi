using ProtoBuf.Extended.Meta;

namespace CQRS.Services
{
    public interface ITransientService
    {
        void Print();
    }
    public class TransientService : ITransientService
    {
        public Guid age = Guid.Empty;
        public TransientService() { age = Guid.NewGuid(); }
        void ITransientService.Print()
        {
            
            Console.WriteLine("TransientService print-"+age);
        }
    }
    public interface ITransientService1
    {
        void Print();
    }
    public class TransientService1 : ITransientService1
    {
        public Guid age = Guid.Empty;
        private readonly IScopedService1 _service;
        public TransientService1(IScopedService1 service) 
        { 
            age = Guid.NewGuid(); 
            _service = service;
        }
        void ITransientService1.Print()
        {
            _service.Print();
            Console.WriteLine("TransientService print-" + age);
        }
    }
    public interface ITransientService2
    {
        void Print();
    }
    public class TransientService2 : ITransientService2
    {
        public Guid age = Guid.Empty;
        private readonly ISingletonService1 _service;
        public TransientService2(ISingletonService1 service)
        {
            age = Guid.NewGuid();
            _service = service;
        }
        void ITransientService2.Print()
        {
            _service.Print();
            Console.WriteLine("TransientService2 print-" + age);
        }
    }
}
