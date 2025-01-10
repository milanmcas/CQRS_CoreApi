namespace CQRS.Services
{
    public interface ISingletonService
    {
        void Print();
    }
    public class SingletonService: ISingletonService
    {
        public Guid Guid { get; set; }
        private readonly ITransientService _service;
        public SingletonService(ITransientService service) {
            Guid= Guid.NewGuid();
            _service = service;
        }

        void ISingletonService.Print()
        {
            Console.WriteLine("ISingletonService.Print - "+Guid);
            _service.Print();
        }        
    }
    public interface ISingletonService1
    {
        void Print();
    }
    public class SingletonService1 : ISingletonService1
    {
        public Guid Guid { get; set; }
        public SingletonService1()
        {
            Guid = Guid.NewGuid();

        }
        public void Print()
        {
            Console.WriteLine("SingletonService1-"+ Guid);
        }
    }
}
