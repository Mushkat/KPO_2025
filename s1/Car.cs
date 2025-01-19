namespace s1
{

    public class Car
    {
        private static int _globalNumber = 0;
        private static readonly Random _random = new();

        public int Number{ get; set; }

        public IEngine Engine { get; }

        public Car()
        {
            Engine = (IEngine)new Engine { Size = _random.Next(1, 10) };
            Number = ++_globalNumber;
        }

        public Car(IEngine engine)
        {
            Engine = engine;
            Number = ++_globalNumber;
        }

        public override string ToString()
        {
            return $"Номер: {Number}, Размер педалей: {Engine.EngineType}";
        }
    }
}