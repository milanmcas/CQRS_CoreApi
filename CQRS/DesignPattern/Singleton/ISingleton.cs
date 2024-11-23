namespace CQRS.DesignPattern.Singleton
{
    public interface ISingleton
    {
        void PrintDetails(string message);
        ISingleton GetInstance();
    }
    public sealed class Singleton
    {
        //This variable value will be increment by 1 each time the object of the class is created
        private static int Counter = 0;
        //This variable is going to store the Singleton Instance
        private static Singleton? Instance;
        private static readonly object padlock = new object();
        //The following Static Method is going to return the Singleton Instance
        public static Singleton GetInstance()
        {
            //If the variable instance is null, then create the Singleton instance 
            //else return the already created singleton instance
            //This version is not thread safe
            lock (padlock)
            {
                if (Instance == null)
                {
                    Instance = new Singleton();
                }
                //Return the Singleton Instance
                return Instance;
            }            
            
        }
        //Constructor is Private means, from outside the class we cannot create an instance of this class
        private Singleton()
        {
            //Each Time the Constructor called, increment the Counter value by 1
            Counter++;
            Console.WriteLine("Counter Value " + Counter.ToString());
        }
        //The following can be accesed from outside of the class by using the Singleton Instance
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
    }
}
