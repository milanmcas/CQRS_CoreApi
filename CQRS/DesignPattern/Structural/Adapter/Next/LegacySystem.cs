namespace CQRS.DesignPattern.Structural.Adapter.Next
{
    // Adaptee: The existing class
    class LegacySystem
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Legacy System's Specific Request");
        }
    }
    // Target: The interface expected by the client
    interface ITarget
    {
        void Request();
    }
    // Adapter: Adapts the Adaptee to the Target interface
    class Adapter : ITarget
    {
        private readonly LegacySystem _legacySystem;

        public Adapter(LegacySystem legacySystem)
        {
            _legacySystem = legacySystem;
        }

        public void Request()
        {
            _legacySystem.SpecificRequest();
        }
    }
    // Client code
    class Client
    {
        public void Main()
        {
            LegacySystem legacySystem = new LegacySystem();
            ITarget adapter = new Adapter(legacySystem);
            adapter.Request();
        }
    }
}
