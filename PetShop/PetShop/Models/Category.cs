using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Display(Name = "Category Name:")]
        [Required(ErrorMessage = "Please enter name for category")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
