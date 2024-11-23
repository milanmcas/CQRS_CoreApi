namespace CQRS.DesignPattern.Structural.Proxy
{
    public class ConcreteSubject:Subject
    {
        private string _user;

        public ConcreteSubject(string user)
        {
            _user = user;
        }

        public override void DoSomeWork()
        {
            Console.WriteLine($"User {_user} is authorized and this text comes from ConcreteSubjectModified.DoSomeWork() method for him.");
        }
    }
}
