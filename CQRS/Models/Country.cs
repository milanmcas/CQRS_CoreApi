namespace CQRS.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<City> City { get; } = new List<City>();//Collection Navigation Property
    }
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FKCountry { get; set; }

        public Country Country { get; set; } = null!;//Reference Navigation Property
    }
}
