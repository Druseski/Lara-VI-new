using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Entities.ViewModels;
using Lara_VI.Repositories.Interfaces;

using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Controllers
{
    [Authorize(Roles=WebConstants.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public ProductController(IProductRepository productRepo, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _productRepo = productRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepo.GetAll(includeProperties: "Category");
            

            return View(products);
        }
        //GET-CREATE
        public IActionResult Upsert(int? id)
        {
      
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _productRepo.GetAllDropdownList(WebConstants.CategoryName)
            };

            if (id == null)
            {
                //This is for Create
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _productRepo.Find(id.GetValueOrDefault()); 
                if (productViewModel.Product == null)
                {
                    return NotFound();
                }
                return View(productViewModel);
            }

        }

        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRoothPath = _webHostEnvironment.WebRootPath;

                if(productViewModel.Product.Id == 0)
                {
                    //Create
                    string upload = webRoothPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productViewModel.Product.Image = fileName + extension;

                    _productRepo.Add(productViewModel.Product);
                    TempData[WebConstants.Success] = "Product added successfully";
                }
                else
                {
                    //Update
                    var productEdit = _productRepo.FirstOrDefault(x => x.Id == productViewModel.Product.Id,isTracking:false);

                    if(files.Count>0)
                    {
                        string upload = webRoothPath + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, productEdit.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productViewModel.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productViewModel.Product.Image = productEdit.Image;
                    }
                    _productRepo.Update(productViewModel.Product);
                    TempData[WebConstants.Success] = "Product edited successfully";
                }
                _productRepo.Save();
                return RedirectToAction("Index");
            }
           productViewModel.CategorySelectList = _productRepo.GetAllDropdownList(WebConstants.CategoryName);

            return View(productViewModel);

        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _productRepo.FirstOrDefault(u => u.Id == id,includeProperties:"Category");
           // product.Category = _dataContext.Category.Find(product.CategoryId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _productRepo.Find(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }
            string webRoothPath = _webHostEnvironment.WebRootPath;
            string upload = webRoothPath + WebConstants.ImagePath;
            
           

            var oldFile = Path.Combine(upload, product.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _productRepo.Remove(product);
            _productRepo.Save();
            TempData[WebConstants.Success] = "Product deleted successfully";
            return RedirectToAction("Index");


        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _productRepo.FirstOrDefault(u => u.Id == id, includeProperties: "Category"); 
            
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}

