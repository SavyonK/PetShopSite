using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IPetService _petService;
        public CatalogController(IPetRepository petRepository, IPetService petService)
        {
            _petRepository = petRepository;
            _petService = petService;
        }
        public IActionResult ShowCatalog()
        {
            ViewBag.AnimalList = _petRepository.GetAnimals();
            return View();
        }

        [HttpGet]
        public IActionResult ShowAnimal(int id)
        {
            Animal selectedAnimal = _petRepository.GetSpecificAnimal(id);
            AnimalDisplayViewModel animalDisplay = new AnimalDisplayViewModel(selectedAnimal, _petService.GetCategoryForAnimal(selectedAnimal.CategoryID));

            //send animal ViewModel to viewbag
            ViewBag.SelectedAnimal = animalDisplay;

            ViewBag.AnimalID = id;
            ViewBag.CommentsList = _petService.GetCommentsForAnimal(id).ToList();


            return View();
        }
        [HttpPost]
        public IActionResult ShowAnimal(Comments input)
        {
            if (ModelState.IsValid)
            {
                _petRepository.InsertComment(input);

                return RedirectToAction("ShowAnimal", "Catalog", input.AnimalID);

            }
            else //user submitted empty comment
            {

                return RedirectToAction("ShowAnimal", "Catalog", input.AnimalID);
            }
        }
    }
}