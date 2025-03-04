namespace CQRS.Models
{
    public record CountryName(int Id, string Name)
    {
        public CountryName():this(0,"")
        {
            
        }
        public static List<CountryName> countryNames = new List<CountryName>()
        {
            new CountryName(1,"Company1"),
            new CountryName(2,"Company12"),
            new CountryName(3,"Cocacola"),
            new CountryName(4,"Cokacola"),
            new CountryName(5,"Charkol"),
            new CountryName(6,"Customer123"),
            new CountryName(7,"Casual"),
            new CountryName(8,"Cascading")
        };
    }
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
