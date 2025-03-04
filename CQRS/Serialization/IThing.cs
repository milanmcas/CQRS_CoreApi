using System.Text.Json;

namespace CQRS.Serialization
{
    public class IthingTest
    {
        public static IEnumerable<IThing> MainMethod()
        {
            IThing one = new One { Name = "One" };
            IThing two = new Two { Name = "Two", Count = 42 };

            var things = new[] { one, two };
            return things;
        }
        public static string MainMethod1()
        {
            IThing one = new One { Name = "One" };
            IThing two = new Two { Name = "Two", Count = 42 };

            object[] things =  { one, two };
            return JsonSerializer.Serialize(things);
        }
    }
    public static class IThingExtension
    {
        public static void Print(this IThing thing)
        {
            Console.WriteLine("Print extension method");
        }
    }
    public interface IThing
    {
        public string Name { get; set; }
    }
    public class One : IThing
    {
        public string Name { get; set; }
    }

    public class Two : IThing
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
