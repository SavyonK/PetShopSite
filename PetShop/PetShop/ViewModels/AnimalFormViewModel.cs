using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    /// <summary>
    /// will act as base form - no field is required so it can be used for animal modification
    /// </summary>
    public class AnimalFormViewModel
    {
        public AnimalFormViewModel()
        {

        }
        public AnimalFormViewModel(Animal animal)
        {
            AnimalID = animal.AnimalID;
            Name = animal.Name;
            Age = animal.Age;
            Description = animal.Description;
            CategoryID = animal.CategoryID;

        }

        public static object WebHostEnviroment { get; private set; }
        public int AnimalID { get; set; }

        [Display(Name = "Name:")]
        [StringLength(30)]
        public virtual string Name { get; set; }

        [Display(Name = "Age:")]
        [Range(0, 700)]
        public virtual int? Age { get; set; }


        [Display(Name = "Photo:")]
        public virtual IFormFile ProfileImage { get; set; }


        [Display(Name = "Description:")]
        [StringLength(50)]
        public virtual string Description { get; set; }

        [Display(Name = "Category ID:")]
        public virtual int? CategoryID { get; set; }    //FK



    }
}
