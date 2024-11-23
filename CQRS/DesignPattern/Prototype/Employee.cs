namespace CQRS.DesignPattern.Prototype
{
    /// <summary>
    /// Prototype design pattern
    /// When creating a new object is going to be costly
    /// Shallow copy and dep copy
    /// </summary>
    public class Employee
    {
        public string? Name { get; set; }
        public string? Department { get; set; }
        public Address? EmpAddress { get; set; }
        public Employee GetClone()
        {
            return (Employee)this.MemberwiseClone();
        }
    }
    public class Address
    {
        public string? address { get; set; }
        public Address GetClone()
        {
            return (Address)this.MemberwiseClone();
        }
    }
    public interface IPrototype
    {
        string Name { get; set; }
        IPrototype Clone();
    }
    public class Prototype : IPrototype
    {
        public string Name { get; set; }
        public IPrototype Clone()
        {
            return (IPrototype)this.MemberwiseClone();
        }
    }
}
