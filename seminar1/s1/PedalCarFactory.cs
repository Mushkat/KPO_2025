namespace s1{

    public class PedalCarFactory
    {
        private readonly CarWarehouse _warehouse;

        public PedalCarFactory(CarWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void CreateCar(int pedalSize)
        {
            var engine = new PedalEngine { PedalSize = pedalSize };
            var car = new Car(engine);
            _warehouse.AddCar(car);
        }
    }
}