namespace Zoo_dz1
{
    public class Zoo
    {
        private readonly IVeterinaryClinic _clinic;
        private List<Animal> _animals = new();
        private List<Thing> _things = new();
        private int _nextNumber = 1;

        public Zoo(IVeterinaryClinic clinic) => _clinic = clinic;

        public bool AddAnimal(Animal animal)
        {
            if (!_clinic.CheckHealth(animal)) return false;
            animal.Number = _nextNumber++;
            _animals.Add(animal);
            return true;
        }

        public void AddThing(Thing thing)
        {
            thing.Number = _nextNumber++;
            _things.Add(thing);
        }

        public IReadOnlyCollection<Animal> GetAnimals()
        {
            return _animals.AsReadOnly();
        }

        public int TotalFood => _animals.Sum(a => a.Food);
        public IEnumerable<Animal> ContactZooAnimals =>
            _animals.OfType<Herbo>().Where(h => h.LevelOfKindness > 5);

        public IEnumerable<Animal> ZooAnimals => _animals;

        public IEnumerable<Thing> ZooThings => _things;
    }
}
