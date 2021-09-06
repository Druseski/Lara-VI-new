using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Entities.ViewModels;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        
        public HomeController(ILogger<HomeController> logger,
            IProductRepository productRepo,
            ICategoryRepository categoryRepo
            )
        {
            _logger = logger;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
            
        }

        public IActionResult Index()
        {

            
            HomeViewModel homeViewModel = new HomeViewModel()
            {

                Products = _productRepo.GetAll(includeProperties:"Category"),
                Categories = _categoryRepo.GetAll()
               
            };

            
            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            HomeDetailsProductViewModel homeDetailsProductViewModel = new HomeDetailsProductViewModel()
            {
                Product = _productRepo.FirstOrDefault(u => u.Id == id,includeProperties: "Category"),
                ExistInCart = false

            };
           

            foreach (var item in shoppingCartList)
            {
                if (item.ProductId == id)
                {
                    
                    homeDetailsProductViewModel.ExistInCart = true;
                }
            }

            return View(homeDetailsProductViewModel);
        }
        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id,HomeDetailsProductViewModel detailsVM)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }
            shoppingCartList.Add(new ShoppingCart { ProductId = id,ByPeace=detailsVM.Product.TempByPeace,ByWeight = detailsVM.Product.TempByWeight });
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);
            }

            var itemToRemove = shoppingCartList.SingleOrDefault(x => x.ProductId == id);
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
