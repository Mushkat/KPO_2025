namespace s1
{
    public class Customer
    {
        public required string Name { get; set; }
        public int LegStrength { get; set; }
        public int ArmStrength { get; set; }

        public Car? Car { get; set; }

        public override string ToString()
        {
            return $"Имя: {Name}, Номер машины: {Car?.Number ?? -1}";
        }
    }
}