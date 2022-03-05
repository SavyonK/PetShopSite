using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModels
{
    /// <summary>
    /// this vm is for creating new animal object - all fields are mandatory
    /// </summary>
    public class AdminAddViewModel : AnimalFormViewModel
    {
        public AdminAddViewModel()
        {

        }



        [Required(ErrorMessage = "Please enter a name")]
        public override string Name { get; set; }

        [Display(Name = "Age:")]
        [Range(0, 700)]
        [Required(ErrorMessage = "Please enter valid age")]
        public new int Age { get; set; }


        [Required(ErrorMessage = "Please insert photo")]
        public override IFormFile ProfileImage { get; set; }


        [Required(ErrorMessage = "Please enter valid description")]
        public override string Description { get; set; }

        [Display(Name = "Category ID:")]
        [Required(ErrorMessage = "Category ID can't be left empty")]
        public new int CategoryID { get; set; }    //FK
    }
}
