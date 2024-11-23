namespace CQRS.OOPS
{
    /// <summary>
    /// If a virtual method is declared abstract, 
    /// it is still virtual to any class inheriting from 
    /// the abstract class. A class inheriting an abstract method 
    /// cannot access the original implementation of the method—in 
    /// the previous example, DoWork on class DerivedFromAbstract cannot call DoWork 
    /// on class NormalClassA. In this way, an abstract class can force derived classes to provide new method implementations for virtual methods.
    /// </summary>
    public class NormalClassA
    {
        public virtual void DoWork(int i)
        {
            Console.WriteLine("NormalClassA - " + i);
        }
    }
    public interface IAbstract
    {
        void Do();
    }
    public abstract class AbstractClass: NormalClassA, IAbstract
    {
        public void Do()
        {
            throw new NotImplementedException();
        }

        public abstract override void DoWork(int i);
        public abstract void Print();
    }
    public class DerivedFromAbstract : AbstractClass
    {
        public override void DoWork(int i)
        {
            Console.WriteLine("DerivedFromAbstract - " + i);
        }

        public sealed override void Print()
        {
            throw new NotImplementedException();
        }
    }
    public static class StaticClass: object
    {
        static void Print()
        {
            
        }
    }
    
}
