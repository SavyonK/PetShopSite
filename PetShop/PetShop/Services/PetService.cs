using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Services
{
    /// <summary>
    /// extension for the basic repository, doesnt change db
    /// </summary>
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;


        }


        /// <summary>
        /// Gets top commented animals
        /// </summary>
        /// <param name="numOfAnimals">if there are not enough animals in db, lower number of animals will be returned</param>
        /// <returns></returns>
        public IEnumerable<Animal> GetTopCommentedAnimals(int numOfAnimals)
        {
            Dictionary<int, int> id_numOfComments = new Dictionary<int, int>();//will contain animal id-num of comments to each animal

            List<Animal> AnimalsWithTopComments = new List<Animal>(); //list to be returned 

            IEnumerable<Comments> comments = _petRepository.GetComments();


            //counting comments per animal
            foreach (var item in comments)
            {
                if (id_numOfComments.ContainsKey(item.AnimalID))
                {
                    ++id_numOfComments[item.AnimalID];
                }
                else
                {
                    id_numOfComments.Add(item.AnimalID, 1);
                }
            }

            //verify that number of animals requested is doesn't exceeds number of animals in total
            int numLimit = numOfAnimals < id_numOfComments.Keys.Count() ? numOfAnimals : id_numOfComments.Keys.Count();

            //getting top commemted animals 
            var sortedDict = (from entry in id_numOfComments
                              orderby entry.Value descending
                              select entry).Take(numLimit).ToList();

            //initializing return list with actual animal objects from repository
            for (int i = 0; i < numLimit; i++)
            {
                int animalID = sortedDict.ElementAt(i).Key;
                Animal result = _petRepository.GetSpecificAnimal(animalID);
                if (result != null)
                {
                    AnimalsWithTopComments.Add(result);
                }
            }

            return AnimalsWithTopComments;
        }

        /// <summary>
        /// Gets list of comments for certain animal 
        /// </summary>
        /// <param name="id">animal id</param>
        /// <returns></returns>
        public IEnumerable<Comments> GetCommentsForAnimal(int id)
        {
            var allComments = _petRepository.GetComments();
            var commentsForAnimal = from c in allComments
                                    where c.AnimalID == id
                                    select c;
            return commentsForAnimal.AsEnumerable();

        }

        /// <summary>
        /// get animal's category for display purposes
        /// </summary>
        /// <param name="categoryID">category id</param>
        /// <returns></returns>
        public Category GetCategoryForAnimal(int categoryID)
        {
            var categories = _petRepository.GetCategories();

            var category = from c in categories
                           where c.CategoryID == categoryID
                           select c;

            return category.FirstOrDefault();
        }

    }
}
