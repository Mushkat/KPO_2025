namespace s1
{

    public class Engine
    {
        public required int Size { get; set; }
    }

    public class PedalEngine : IEngine
    {
        public int PedalSize { get; set; }

        public string EngineType => "Pedal";

        public bool IsCompatible(Customer customer) => customer.LegStrength > 5;
    }

    public class HandEngine : IEngine
    {
        public string EngineType => "Hand";

        public bool IsCompatible(Customer customer) => customer.ArmStrength > 5;
    }
}