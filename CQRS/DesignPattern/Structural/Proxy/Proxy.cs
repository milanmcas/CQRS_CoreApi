namespace CQRS.DesignPattern.Structural.Proxy
{
    public class Proxy:Subject
    {
        Subject? cs;
        string[] registeredUsers;
        string currentUser;

        public Proxy(string currentUser)
        {
            //Avoiding to instantiate inside the constructor
            //cs = new ConcreteSubject();
            //Registered users
            registeredUsers = new string[] { "Admin", "Manager" };
            this.currentUser = currentUser;
        }
        public override void DoSomeWork()
        {
            Console.WriteLine("\nProxy call happening now...");
            Console.WriteLine("{0} wants to invoke a proxy method.", currentUser);

            if (registeredUsers.Contains(currentUser))
            {
                //Lazy initialization: We'll not instantiate until the method is called
                if (cs == null)
                {
                    cs = new ConcreteSubject(currentUser);
                }
                cs.DoSomeWork();
            }
            else
            {
                Console.WriteLine("Sorry {0}, you do not have access.", currentUser);
            }
        }
    }
}
