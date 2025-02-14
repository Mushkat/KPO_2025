using Microsoft.Extensions.DependencyInjection;
using Zoo_dz1.Things;

namespace Zoo_dz1
{
    class Program()
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
            services.AddSingleton<Zoo>();
            var provider = services.BuildServiceProvider();
            var zoo = provider.GetService<Zoo>();

            var monkey = new Monkey("Chipi", 12);
            var rabbit = new Rabbit("Malish", 7);
            var tiger = new Tiger("Rizhik");
            var tiger2 = new Tiger("Polosatik");
            var wolf = new Wolf("Pushok");

            if (zoo.AddAnimal(monkey))
            {
                Console.WriteLine("Обезъяна добавлена!");
            }
            else 
            { 
                Console.WriteLine("Животное еще болеет");
            }

            if (zoo.AddAnimal(rabbit)) 
            { 
                Console.WriteLine("Кролик добавлен!"); 
            }
            else
            {
                Console.WriteLine("Животное еще болеет");
            }

            if (zoo.AddAnimal(tiger))
            { 
                Console.WriteLine("Тигр добавлен!"); 
            }
            else
            {
                Console.WriteLine("Животное еще болеет");
            }

            if (zoo.AddAnimal(tiger2))
            {
                Console.WriteLine("Второй тигр добавлен!");
            }
            else
            {
                Console.WriteLine("Животное еще болеет");
            }

            if (zoo.AddAnimal(wolf))
            { 
                Console.WriteLine("Волк добавлен!");
            }
            else
            {
                Console.WriteLine("Животное еще болеет");
            }

            Console.WriteLine($"Общий расход еды: {zoo.TotalFood}");
            Console.WriteLine("Животные для контактного зоопарка:");
            foreach (var animal in zoo.ContactZooAnimals)
            {
                Console.WriteLine($"Животное №{animal.Number} (доброта: {(animal as Herbo).LevelOfKindness})");
            }

            Console.WriteLine("Все животные в зоопарке:");
            foreach (var animal in zoo.ZooAnimals)
            {
                Console.WriteLine($"Животное №{animal.Number} {animal.Name}");
            }

            var table = new Table(1, "Стол администратора", "Дерево");
            var computer = new Computer(2, "Компьютер бухгалтера", "Intel i7");

            Console.WriteLine("Вещи в кладовке зоопарка:");
            Console.WriteLine($"№{table.Number}: {table.Name}, материал: {table.Material}");
            Console.WriteLine($"№{computer.Number}: {computer.Name}, процессор: {computer.ProcessorType}");
        }
    }
}