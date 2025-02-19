namespace CQRS.SOLID
{
    /// <summary>
    /// Behavioral Consistency: Subclasses must adhere to the behavior defined by their base classes.
    /// No Surprises: A subclass should not override or weaken any functionality of the base class.
    /// Contracts: Subclasses should honor the "contract" (e.g., preconditions and postconditions) established by the base class.
    /// </summary>
    public class LSP3
    {
        public static void Main()
        {
            Shape rectangle = new Rectangle { Width = 4, Height = 5 };
            Shape square = new Square { SideLength = 4 };

            Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}");
            Console.WriteLine($"Square Area: {square.GetArea()}");
        }
    }
    public abstract class Shape
    {
        public abstract double GetArea();
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double GetArea() => Width * Height;
    }

    public class Square : Shape
    {
        public double SideLength { get; set; }

        public override double GetArea() => SideLength * SideLength;
    }

    public class LSPAdherenceDemo
    {
        public static void Main()
        {
            Shape rectangle = new Rectangle { Width = 4, Height = 5 };
            Shape square = new Square { SideLength = 4 };

            Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}");
            Console.WriteLine($"Square Area: {square.GetArea()}");
        }
    }
}
