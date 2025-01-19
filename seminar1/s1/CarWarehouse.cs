namespace s1
{
    public class CarWarehouse
    {
        private readonly List<Car> _cars = new();

        public void AddCar(Car car) => _cars.Add(car);

        public Car? GetCar()
        {
            if (_cars.Count == 0) return null;
            var car = _cars[0];
            _cars.RemoveAt(0);
            return car;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _cars);
        }
    }
}