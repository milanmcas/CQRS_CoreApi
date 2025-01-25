namespace CQRS.Models
{
    public class City1
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CityInformation CityInformation { get; set; }
    }
    public class CityInformation
    {
        public int Id { get; set; }

        public int Population { get; set; }

        public string OtherName { get; set; }

        public string MayorName { get; set; }

        public int CityId { get; set; }

        public City1 City { get; set; } = null!;
    }
}
