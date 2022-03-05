using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PetShop.Data;
using PetShop.Models;
using PetShop.Interfaces;
using Microsoft.AspNetCore.Razor.Language;
using System.IO;
using PetShop.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace PetShop.Services
{

    public class PetRepository : IPetRepository
    {
        private PetContext _petContext;
        private readonly IWebHostEnvironment WebHostEnviroment;
        public PetRepository(PetContext petContext, IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnviroment = webHostEnvironment;
            _petContext = petContext;
        }

        /// <summary>
        /// removes animal from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAnimal(int id)
        {
            var result = _petContext.Animals.Where(item => item.AnimalID == id);

            if (result.FirstOrDefault() is Animal)
            {
                Animal animal = (Animal)result.FirstOrDefault();
                _petContext.Animals.Remove(animal);
                SaveChanges();

                DeleteCommentsForAnimal(id);
            }
            else
            {
                throw new InvalidCastException($"Item retrieved for ID-{id} is not of type Animal. Removing from DB failed.");
            }
        }

        /// <summary>
        /// should be invoked when deleting an animal
        /// </summary>
        /// <param name="id">animal ID</param>
        private void DeleteCommentsForAnimal(int id)
        {
            var result = _petContext.CommentsDbSet.Where(item => item.AnimalID == id);
            foreach (var item in result)
            {
                if (item is Comments)
                {
                    Comments comment = (Comments)item;
                    _petContext.CommentsDbSet.Remove(comment);
                }
            }
            SaveChanges();
        }

        /// <summary>
        /// removes catagory from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCategory(int id)
        {
            var result = _petContext.Categories.Where(item => item.CategoryID == id);

            if (result is Category)
            {
                Category category = (Category)result;
                _petContext.Categories.Remove(category);
                SaveChanges();
            }
            else
            {
                throw new InvalidCastException($"Item retrieved for ID-{id} is not of type Category. Removing from DB failed.");
            }

        }

        /// <summary>
        /// removes comment from db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteComment(int id)
        {
            var result = _petContext.CommentsDbSet.Where(item => item.CommentsID == id);

            if (result is Comments)
            {
                Comments comment = (Comments)result;
                _petContext.CommentsDbSet.Remove(comment);
                SaveChanges();
            }
            else
            {
                throw new InvalidCastException($"Item retrieved for ID-{id} is not of type Comments. Removing from DB failed.");
            }

        }

        /// <summary>
        /// gets all animals from db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Animal> GetAnimals()
        {
            return _petContext.Animals.ToList();
        }

        /// <summary>
        /// gets all catagories from db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategories()
        {
            return _petContext.Categories.ToList();
        }

        /// <summary>
        /// gets all comments from db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Comments> GetComments()
        {
            return _petContext.CommentsDbSet.ToList();
        }


        /// <summary>
        /// insert animal to db
        /// </summary>
        /// <param name="animal"></param>
        public void InsertAnimal(Animal animal)
        {
            _petContext.Animals.Add(animal);
            SaveChanges();

        }

        /// <summary>
        /// insert catagory to db
        /// </summary>
        /// <param name="category"></param>
        public void InsertCategory(Category category)
        {
            _petContext.Categories.Add(category);
            SaveChanges();
        }

        /// <summary>
        /// insert comment to db
        /// </summary>
        /// <param name="comment"></param>
        public void InsertComment(Comments comment)
        {
            _petContext.CommentsDbSet.Add(comment);
            SaveChanges();
        }

        /// <summary>
        /// update animal in db
        /// </summary>
        /// <param name="animal"></param>
        public void UpdateAnimal(Animal animal)
        {
            _petContext.Animals.Update(animal);
            SaveChanges();
        }

        /// <summary>
        /// update catagory in db
        /// </summary>
        /// <param name="category"></param>
        public void UpdateCategory(Category category)
        {
            _petContext.Categories.Update(category);
            SaveChanges();
        }

        /// <summary>
        /// update comment in db
        /// </summary>
        /// <param name="comment"></param>
        public void UpdateComment(Comments comment)
        {
            _petContext.CommentsDbSet.Update(comment);
            SaveChanges();
        }

        /// <summary>
        /// get animal from db 
        /// </summary>
        /// <param name="animalID"></param>
        /// <returns></returns>
        public Animal GetSpecificAnimal(int animalID)
        {
            List<Animal> animals = GetAnimals().ToList();
            var result = from a in animals
                         where a.AnimalID == animalID
                         select a;
            if (result.First() != null)
            {
                return result.First();
            }
            return new Animal();
        }
  
        /// <summary>
        /// get photo from user input & copies to wwwroot
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>string to insert into Animal.ProfileImage</returns>
        public string UploadFile(AnimalFormViewModel vm)
        {
            string filename = null;
            if (vm.ProfileImage != null)
            {
                string uploadDir = Path.Combine(WebHostEnviroment.WebRootPath, "Images");//wwwroot/Images
                filename = Guid.NewGuid().ToString() + "-" + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);
                }
            }
            return filename;
        }

        private void SaveChanges()
        {
            _petContext.SaveChanges();
        }
    }
}
