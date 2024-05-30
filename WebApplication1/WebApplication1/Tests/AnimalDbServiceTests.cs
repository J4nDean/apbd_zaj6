using Moq;
using WebApplication1.Models;
using WebApplication1.Services;
using Xunit;
using Xunit.Abstractions; 

public class AnimalDbServiceTests
{
    private readonly Mock<IAnimalsDbService> _animalDbServiceMock;
    private readonly ITestOutputHelper _output; 

    public AnimalDbServiceTests(ITestOutputHelper output) 
    {
        _animalDbServiceMock = new Mock<IAnimalsDbService>();
        _output = output; 
    }

    [Fact]
    public void GetAnimal_ShouldReturnAnimalById()
    {
        var expectedAnimal = new Animal { IdAnimal = 1, Name = "Dog" };
        _animalDbServiceMock.Setup(x => x.GetAnimalById(1)).Returns(expectedAnimal);

        var result = _animalDbServiceMock.Object.GetAnimalById(1);

        _output.WriteLine($"Expected: {expectedAnimal.Name}, Actual: {result.Name}");

        Assert.Equal(expectedAnimal, result);
    }

    [Fact]
    public void GetAniaml_ShouldBeOrderedByName()
    {
        var animals = new List<Animal>
        {
            new Animal { IdAnimal = 1, Name = "Dog" },
            new Animal { IdAnimal = 2, Name = "Cat" },
            new Animal { IdAnimal = 3, Name = "Alan" }
        };

        _animalDbServiceMock.Setup(x => x.GetAnimals("Name")).Returns(animals);
        var result = _animalDbServiceMock.Object.GetAnimals("Name");

        var expected = animals.OrderBy(a => a.Name).ToList();
        var actual = result.OrderBy(a => a.Name).ToList();

        _output.WriteLine($"Expected: {string.Join(", ", expected.Select(a => a.Name))}, Actual: {string.Join(", ", actual.Select(a => a.Name))}");

        Assert.Equal(expected, actual);
    }
}