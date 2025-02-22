namespace CQRS.OOPS
{
    public class TestInheritance
    {
        public static void MainMethod()
        {
            InheritanceClass inheritanceClass = new SecondDerivedClass();
            inheritanceClass.Print();
            inheritanceClass.Write();
            inheritanceClass.Method1();
        }
    }
    public class InheritanceClass
    {
        public InheritanceClass()
        {
            Console.WriteLine("InheritanceClass constructor");
        }
        static InheritanceClass()
        {
            Console.WriteLine("InheritanceClass static constructor");
        }
        public virtual void Print()
        {
            Console.WriteLine("InheritanceClass Print");
        }
        public virtual void Write()
        {
            Console.WriteLine("InheritanceClass Write");
        }
        public void Method1()
        {
            Console.WriteLine("InheritanceClass Method1");
        }
    }
    public class FirstDerivedClass: InheritanceClass
    {
        public FirstDerivedClass()
        {
            Console.WriteLine("FirstDerivedClass constructor");
        }
        static FirstDerivedClass()
        {
            Console.WriteLine("FirstDerivedClass static constructor");
        }
        public override void Print()
        {
            Console.WriteLine("FirstDerivedClass Print");
        }
        public override void Write()
        {
            Console.WriteLine("FirstDerivedClass Write");
        }
        public new void Method1()
        {
            Console.WriteLine("FirstDerivedClass Method1");
        }
    }
    public class SecondDerivedClass : FirstDerivedClass
    {
        public SecondDerivedClass()
        {
            Console.WriteLine("SecondDerivedClass constructor");
        }
        static SecondDerivedClass()
        {
            Console.WriteLine("SecondDerivedClass static constructor");
        }
        public new void Print()
        {
            Console.WriteLine("SecondDerivedClass Print");
        }
        public new void Write()
        {
            Console.WriteLine("SecondDerivedClass Print");
        }
    }
}
