using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;

using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
          
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepo.GetAll();
            return View(categories);
        }
        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData[WebConstants.Success] = "Category created successfully";
                return RedirectToAction("Index");
            }
            TempData[WebConstants.Error] = "Error while creating Category";
            return View(category);

        }

        //GET-Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _categoryRepo.Find(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData[WebConstants.Success] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            TempData[WebConstants.Error] = "Error while editing Category";
            return View(category);

        }
        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _categoryRepo.Find(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _categoryRepo.Find(id.GetValueOrDefault());
            if (category == null)
            {
                TempData[WebConstants.Success] = "Error while deleting Category";
                return NotFound();
            }

            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData[WebConstants.Success] = "Category Delited successfully";
            
            return RedirectToAction("Index");


        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _categoryRepo.Find(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

       
    }
}
