using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShop.Models;
namespace PetShop.Interfaces
{
    public interface IPetService
    {
        IEnumerable<Animal> GetTopCommentedAnimals(int numOfAnimals);
        public IEnumerable<Comments> GetCommentsForAnimal(int id);
        public Category GetCategoryForAnimal(int categoryID);
    }
}
