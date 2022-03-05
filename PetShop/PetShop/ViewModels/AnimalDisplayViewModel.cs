using PetShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class AnimalDisplayViewModel
    {
        public AnimalDisplayViewModel(Animal animal,Category category)
        {
            AnimalID = animal.AnimalID;
            Name = animal.Name;
            Age = animal.Age;
            ProfileImage = animal.ProfileImage;
            Description = animal.Description;
            Category = category.Name;
        }
        [Display(Name = "Animal ID:")]
        public int AnimalID { get; set; }

        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Age:")]

        public int Age { get; set; }


        [Display(Name = "Photo:")]
        public string ProfileImage { get; set; }


        [Display(Name = "Description:")]
        public string Description { get; set; }


        [Display(Name = "Category:")]
        public string Category { get; set; }


     
    }
}
