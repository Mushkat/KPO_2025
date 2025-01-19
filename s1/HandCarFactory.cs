namespace s1
{

    public class HandCarFactory
    {
        private readonly CarWarehouse _warehouse;

        public HandCarFactory(CarWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void CreateCar()
        {
            var engine = new HandEngine();
            var car = new Car(engine);
            _warehouse.AddCar(car);
        }
    }
}