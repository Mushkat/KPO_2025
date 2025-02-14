namespace Zoo_dz1.Things
{
    public class Computer : Thing
    {
        public string ProcessorType { get; }

        public Computer(int number, string name, string processorType)
            : base(number, name)
        {
            ProcessorType = processorType;
        }
    }
}
