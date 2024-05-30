
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAnimalDbService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        Animal GetAnimalById(int id);
        void AddAnimal(Animal newAnimal);
        void UpdateAnimal(Animal updatedAnimal);
        void DeleteAnimal(int id);
    }
}