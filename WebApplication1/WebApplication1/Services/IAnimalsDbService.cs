using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAnimalsDbService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        void AddAnimal(Animal newAnimal);
        void UpdateAnimal(Animal updatedAnimal);
        void DeleteAnimal(int id);
    }
}