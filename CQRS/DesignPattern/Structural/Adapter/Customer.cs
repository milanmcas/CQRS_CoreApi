using CQRS.DesignPattern.Prototype;

namespace CQRS.DesignPattern.Structural.Adapter
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public Customer(int id, string name, decimal salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }
    }
    public interface IEmployeeService
    {
        List<Customer> GetEmployees();
    }
    public class EmployeeService : IEmployeeService
    {
        public List<Customer> GetEmployees()
        {
            return new List<Customer>()
            {
                new Customer(1, "James", 20000),
                new Customer(2, "Peter", 30000),
                new Customer(3, "David", 40000),
                new Customer(4, "George", 50000)
            };
        }
    }
}
