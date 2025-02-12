namespace CQRS.Models
{
    public record EmpSalary
    {
        public int id { get; set; }
        public string EmpName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Salary { get; set; }
    }
}
