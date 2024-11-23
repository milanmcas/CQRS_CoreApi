namespace CQRS.DesignPattern.Structural.Proxy.Another
{
    public interface ISubject
    {
        void Request();
    }
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }
    public class Proxy : ISubject
    {
        private RealSubject? _realSubject;

        public void Request()
        {
            if (_realSubject == null)
            {
                _realSubject = new RealSubject();
            }

            _realSubject.Request();
        }
    }
}
