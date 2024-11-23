namespace CQRS.DesignPattern.Builder
{
    public class House
    {
        public int NumberOfWindows { get; set; }
        public int NumberOfDoors { get; set; }
        public bool HasGarden { get; set; }

        public override string ToString()
        {
            return $"House with {NumberOfWindows} windows, {NumberOfDoors} doors, and " + (HasGarden ? "a garden." : "no garden.");
        }
    }
}
