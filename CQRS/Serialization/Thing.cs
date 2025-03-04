using System.Text.Json.Serialization;

namespace CQRS.Serialization
{
    public class ThingAbstractTest
    {
        public static IEnumerable<ThingAbstract> MainMethod()
        {
            ThingAbstract one = new One1 { Name = "One" };
            ThingAbstract two = new Two1 { Name = "Two", Count = 42 };

            var things = new[] { one, two };
            return things;
        }
    }
    [JsonDerivedType(typeof(Two1))]
    [JsonDerivedType(typeof(One1))]
    public class ThingAbstract
    {
        public string Name { get; set; }
    }
    public class One1 : ThingAbstract
    {
        //public string Name { get; set; }
    }

    public class Two1 : ThingAbstract
    {
        public int Count { get; set; }
        //public string Name { get; set; }
    }
}
