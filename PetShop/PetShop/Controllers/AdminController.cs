using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.Controllers
{
    public class AdminController : Controller
    {
        #region private fields
        #region for tempdata
        private const string FROM_DELETE_ACTION = "AnimalWasDeleted";
        private const string FROM_ADD_ANIMAL_ACTION = "AnimalWasAdded";
        private const string FROM_MODIFY_ACTION = "AnimalWasModified";
        private const string FROM_MODIFY_ACTION_NOT_MODIFIED = "AnimalWasntModified";
        #endregion

        #region services
        private readonly IPetRepository _petRepository;
        private readonly IWebHostEnvironment WebHostEnviroment;
        #endregion

        private readonly IEnumerable<SelectListItem> CategoriesDisplay;
        #endregion

        public AdminController(IPetRepository petRepository, IWebHostEnvironment webHostEnvironment)
        {
            _petRepository = petRepository;
            WebHostEnviroment = webHostEnvironment;
            CategoriesDisplay = _petRepository
                        .GetCategories()
                        .Select(c => new SelectListItem(c.Name, c.CategoryID.ToString()))
                        .ToList();
        }

        public IActionResult Index()
        {
            #region set message alert
            if (TempData[FROM_DELETE_ACTION] != null)
            {
                ViewBag.Message = "Animal was deleted successfully";
            }
            else if (TempData[FROM_ADD_ANIMAL_ACTION] != null)
            {
                ViewBag.Message = "Animal was added successfully";
            }
            else if (TempData[FROM_MODIFY_ACTION] != null)
            {
                object tempDataValue = TempData[FROM_MODIFY_ACTION];

                ViewBag.Message = (bool)tempDataValue ? "Animal was modified successfully" : "An Error occured.\nPlease try again.";
            }
            else if (TempData[FROM_MODIFY_ACTION_NOT_MODIFIED] != null)
            {
                ViewBag.Message = "Try not to submit an empty form next time.";
            }
            #endregion

            ViewBag.AnimalList = _petRepository.GetAnimals();
            return View();

        }


        /// <summary>
        /// Will Return the modify animal view
        /// </summary>
        /// <param name="id"></param>
        public IActionResult Modify(int id)
        {
            ViewBag.Categories = CategoriesDisplay;
            ViewBag.AnimalID = id;
            AnimalFormViewModel viewModel = new AnimalFormViewModel(_petRepository.GetSpecificAnimal(id));
            return View(viewModel);
        }

        /// <summary>
        /// Will Update the animal
        /// </summary>
        /// <param name="adminViewModel"></param>
        [HttpPost]
        public IActionResult Modify(AnimalFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                bool wasActuallyChanged = false;

                Animal animal = _petRepository.GetSpecificAnimal(vm.AnimalID);

                #region check for changes
                if (!string.IsNullOrEmpty(vm.Name) && vm.Name != animal.Name)
                {
                    animal.Name = vm.Name;
                    wasActuallyChanged = true;
                }
                if (!string.IsNullOrEmpty(vm.Description) && animal.Description != vm.Description)
                {
                    animal.Description = vm.Description;
                    wasActuallyChanged = true;
                }
                if (vm.Age != null && animal.Age != (int)vm.Age)
                {
                    animal.Age = (int)vm.Age;
                    wasActuallyChanged = true;
                }
                if (vm.CategoryID != null && animal.CategoryID != (int)vm.CategoryID)
                {
                    animal.CategoryID = (int)vm.CategoryID;
                    wasActuallyChanged = true;
                }
                if (vm.ProfileImage != null)
                {
                    animal.ProfileImage = _petRepository.UploadFile(vm);
                    wasActuallyChanged = true;
                }
                #endregion

                if (wasActuallyChanged) //was changed
                {
                    _petRepository.UpdateAnimal(animal);

                    TempData[FROM_MODIFY_ACTION] = true;
                }
                else //wasnt changed
                {
                    TempData[FROM_MODIFY_ACTION_NOT_MODIFIED] = true;
                }

                return RedirectToAction("Index");
            }

            TempData[FROM_MODIFY_ACTION] = false;

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            _petRepository.DeleteAnimal(id);
            TempData[FROM_DELETE_ACTION] = true;
            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Categories = CategoriesDisplay;
            return View();
        }


        [HttpPost]
        public IActionResult Add(AdminAddViewModel vm)
        {

            if (ModelState.IsValid)
            {
                Animal animal = new Animal()
                {
                    Name = vm.Name,
                    Age = vm.Age,
                    CategoryID = vm.CategoryID,
                    Description = vm.Description,
                    ProfileImage = _petRepository.UploadFile(vm)
                };
                _petRepository.InsertAnimal(animal);

                TempData[FROM_ADD_ANIMAL_ACTION] = true;

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Categories = CategoriesDisplay;
                return View();
            }
        }


    }
}