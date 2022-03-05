using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Animal
    {

        [Display(Name = "Animal ID:")]
        public int AnimalID { get; set; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Age:")]
        [Required(ErrorMessage = "Please enter valid age")]
        [Range(0, 700)]
        public int Age { get; set; }


        [Display(Name = "Photo:")]
        [Required(ErrorMessage = "Please insert photo")]
        public string ProfileImage { get; set; }


        [Display(Name = "Description:")]
        [Required(ErrorMessage = "Please enter valid description")]
        [StringLength(50)]
        public string Description { get; set; }

        [Display(Name = "Category ID:")]
        [Required(ErrorMessage = "Category ID can't be left empty")]
        public int CategoryID { get; set; }    //FK

        public Animal()
        {

        }


    }
}
