using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.Services;
using PetShop.ViewModels;

namespace PetShop.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IPetService _petService;

        public HomeController(IPetService petService)
        {
            _petService = petService;
        }
        public IActionResult ContentsList()
        {
            return View();
        }

        public IActionResult Index() //show 2 favorite animals
        {

            IEnumerable<Animal> topAnimals = _petService.GetTopCommentedAnimals(2);
            
            List<AnimalDisplayViewModel> topAnimalDisplays = new List<AnimalDisplayViewModel>();
            
            foreach (var item in topAnimals)
            {
                topAnimalDisplays.Add(new AnimalDisplayViewModel(item, _petService.GetCategoryForAnimal(item.CategoryID)));
            }
            
            ViewBag.HomeTitle = topAnimalDisplays.Count > 1 ? $"Top {topAnimalDisplays.Count} Commented Animals Are:" : "Top Commented Animal Is:";

            return View(topAnimalDisplays);
        }
    }
}