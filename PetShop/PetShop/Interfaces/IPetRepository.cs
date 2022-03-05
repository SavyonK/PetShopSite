using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.Interfaces
{
    public interface IPetRepository
    {
        IEnumerable<Animal> GetAnimals();
        void InsertAnimal(Animal animal);
        void UpdateAnimal(Animal animal);
        void DeleteAnimal(int id);

        IEnumerable<Category> GetCategories();
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);


        IEnumerable<Comments> GetComments();
        void InsertComment(Comments comment);
        void UpdateComment(Comments comment);
        void DeleteComment(int id);

        Animal GetSpecificAnimal(int animalID);

        string UploadFile(AnimalFormViewModel vm);
    }
}
