namespace s1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var warehouse = new CarWarehouse();
            var customerStorage = new CustomerStorage();
            var shop = new HseCarShop(warehouse, customerStorage);

            var pedalFactory = new PedalCarFactory(warehouse);
            var handFactory = new HandCarFactory(warehouse);

            pedalFactory.CreateCar(6);
            pedalFactory.CreateCar(7);
            handFactory.CreateCar();

            customerStorage.AddCustomer(new Customer { Name = "Ivan", LegStrength = 6 });
            customerStorage.AddCustomer(new Customer { Name = "Petr", ArmStrength = 7 });
            customerStorage.AddCustomer(new Customer { Name = "Sidr", LegStrength = 4, ArmStrength = 4 });

            Console.WriteLine("Before Sale:");
            Console.WriteLine($"Cars in Warehouse:\n{warehouse}");
            Console.WriteLine($"Customers:\n{customerStorage}");

            shop.SaleCar();

            Console.WriteLine("\nAfter Sale:");
            Console.WriteLine($"Cars in Warehouse:\n{warehouse}");
            Console.WriteLine($"Customers:\n{customerStorage}");
        }
    }
}