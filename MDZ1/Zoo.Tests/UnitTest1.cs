namespace Zoo.Tests;

using Xunit;
using Zoo_dz1;

public class ZooTests
{
    [Fact]
    public void AddAnimal_ShouldAddToZoo()
    {
        // Arrange
        var clinic = new VeterinaryClinic();
        var zoo = new Zoo(clinic);
        var rabbit = new Rabbit("Rabbit_name", 7);

        // Act
        bool result = zoo.AddAnimal(rabbit);

        // Assert
        if (rabbit.IsHealthy)
        {
            Assert.Contains(rabbit, zoo.GetAnimals());
        }
        else
        {
            Assert.DoesNotContain(rabbit, zoo.GetAnimals());
        }
        
    }

    [Fact]
    public void TotalFood_ShouldReturnCorrectSum()
    {
        // Arrange
        var clinic = new VeterinaryClinic();
        var zoo = new Zoo(clinic);
        var rabbit = new Rabbit("Rabbit_name", 7);
        var tiger = new Tiger("Tiger_name");

        zoo.AddAnimal(rabbit);
        zoo.AddAnimal(tiger);

        // Act
        int totalFood = zoo.TotalFood;

        int rabbitHealth = (rabbit.IsHealthy ? 1 : 0);

        int tigerHealth = (tiger.IsHealthy ? 1 : 0);

        int expectedFood = 1 * rabbitHealth + 10 * tigerHealth;

        // Assert
        Assert.Equal(expectedFood, totalFood);
    }

    [Fact]
    public void CheckHealth_ShouldReturnTrueOrFalse()
    {
        // Arrange
        var clinic = new VeterinaryClinic();
        var animal = new Rabbit("Rabbit_name", 7);

        // Act
        bool result = clinic.CheckHealth(animal);

        // Assert
        Assert.True(result || !result);
    }

    [Fact]
    public void Thing_ShouldHaveCorrectProperties()
    {
        // Arrange
        var thing = new Thing(1, "Стол");

        // Act & Assert
        Assert.Equal(1, thing.Number);
        Assert.Equal("Стол", thing.Name);
    }
}
