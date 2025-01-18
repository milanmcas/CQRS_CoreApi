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
    /// <summary>
    /// Encapsulation
    /// </summary>
    public class Dog
    {
        private string name;
        private string breed;

        public string Breed { 
            get { return breed; } 
            set { breed = value; }
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }
    }
    /// <summary>
    /// Inheritance
    /// </summary>
    //public class Animal
    //{
    //    public string name;
    //    public void Print() { }
    //}
    //public class Cat : Animal
    //{
    //    public string breed;
    //    public void Add() { }
    //}

    /// <summary>
    /// Polyphormism
    /// In this example, the `Dog` class overrides the `Sound` method of the `Animal` class. 
    /// This allows us to make the `Dog` object perform its own `Sound` action, even when called with an `Animal` reference.
    /// </summary>
    public class Animal
    {
        public virtual void Sound()
        {
            Console.WriteLine("The animal makes a sound");
        }
    }   
    public class Cat: Animal
    {
        public override void Sound()
        {
            Console.WriteLine("The cat barks");
        }
    }
    /// <summary>
    /// Abstraction
    /// </summary>
    public abstract class Bird
    {
        public abstract void Sound();
    }
    public class Parrot : Bird
    {
        public override void Sound()
        {
            Console.WriteLine("The dog barks");
        }
    }
}
