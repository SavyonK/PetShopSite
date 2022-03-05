using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Comments
    {
        public int CommentsID { get; set; }

        [Display(Name = "Comment here:")]
        [Required(ErrorMessage = "Comment given is invalid")]
        [StringLength(250)]
        public string Comment { get; set; }
        public int AnimalID { get; set; }
    }
}
