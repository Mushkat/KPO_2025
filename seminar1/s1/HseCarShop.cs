namespace s1
{
    public class HseCarShop
    {
        private readonly CarWarehouse _warehouse;
        private readonly CustomerStorage _customerStorage;

        public HseCarShop(CarWarehouse warehouse, CustomerStorage customerStorage)
        {
            _warehouse = warehouse;
            _customerStorage = customerStorage;
        }

        public void SaleCar()
        {
            foreach (var customer in _customerStorage.GetCustomers())
            {
                var car = _warehouse.GetCar();
                if (car == null || !car.Engine.IsCompatible(customer)) continue;

                customer.Car = car;
            }
        }
    }
}