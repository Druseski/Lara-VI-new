
using Lara_VI.Entities;
using Lara_VI.Entities.ViewModels;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lara_VI.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        //private readonly IProductService _productService;
        private readonly IUserRepository _userRepo;
        private readonly IInquiryHeaderRepository _inquiryHeaderRepo;
        private readonly IInquiryDetailRepository _inquiryDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;


        public ShoppingCartController(IUserRepository userRepo,
            IInquiryHeaderRepository inquiryHeaderRepo,
            IInquiryDetailRepository inquiryDetailRepo,
            IWebHostEnvironment webHostEnvironment,
            IEmailSender emailSender,
            IProductRepository productRepo)
        {
            _userRepo = userRepo;
            _inquiryHeaderRepo = inquiryHeaderRepo;
            _inquiryDetailRepo = inquiryDetailRepo;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _productRepo = productRepo;

        }

        [BindProperty]
        public ProductUserViewModel ProductUserViewModel { get; set; }



        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                //Session exsist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }

            List<int> productsInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productListTemp = _productRepo.GetAll(u => productsInCart.Contains(u.Id));
            List<Product> productList = new List<Product>();

            foreach (var cartItem in shoppingCartList)
            {
                Product prodTemp = productListTemp.FirstOrDefault(u => u.Id == cartItem.ProductId);
                prodTemp.TempByPeace = cartItem.ByPeace;
                prodTemp.TempByWeight = cartItem.ByWeight;
                productList.Add(prodTemp);
            }
            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<Product> productList)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            foreach (Product product in productList)
            {
                shoppingCartList.Add(new ShoppingCart { ProductId = product.Id, ByPeace = product.TempByPeace, ByWeight = product.TempByWeight });
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            User user;
            if (User.IsInRole(WebConstants.AdminRole))
            {
                if (HttpContext.Session.Get<int>(WebConstants.SessionInquiryId) != 0)
                {
                    //cart has been loaded using inquiry
                    InquiryHeader inquiryHeader = _inquiryHeaderRepo.FirstOrDefault(u => u.Id == HttpContext.Session.Get<int>(WebConstants.SessionInquiryId));
                    user = new User()
                    {
                        Email = inquiryHeader.Email,
                        FullName = inquiryHeader.FullName,
                        PhoneNumber = inquiryHeader.PhoneNumber,
                        Address = inquiryHeader.Address,
                        City = inquiryHeader.City

                    };
                }
                else
                {
                    user = new User();
                }
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                user = _userRepo.FirstOrDefault(u => u.Id == claim.Value);
            }



            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                //Session exsist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }

            List<int> productsInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> productList = _productRepo.GetAll(u => productsInCart.Contains(u.Id));

            ProductUserViewModel = new ProductUserViewModel()
            {
                User = user,
                
            };

            foreach(var cartProducts in shoppingCartList)
            {
                Product productTemp = _productRepo.FirstOrDefault(u=>u.Id==cartProducts.ProductId);
                productTemp.TempByPeace = cartProducts.ByPeace;
                productTemp.TempByWeight = cartProducts.ByWeight;
                ProductUserViewModel.ProductList.Add(productTemp);
            }

            return View(ProductUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserViewModel productUserViewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                 + "templates" + Path.DirectorySeparatorChar.ToString() +
                 "Inquiry.html";

            var subject = "New Inquiry";
            string HtmlBody = "";
            using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }
            //Name: { 0}
            //Email: { 1}
            //Phone: { 2}
            //Products: {3}

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var product in productUserViewModel.ProductList)
            {
                stringBuilder.Append($" - Name:{product.Name}<span style='font-size:14px;'>(ID:{product.Id})</span> <br />");
            }
            string messageBody = string.Format(HtmlBody,
                productUserViewModel.User.FullName,
                productUserViewModel.User.Email,
                productUserViewModel.User.PhoneNumber,
                stringBuilder.ToString());

            await _emailSender.SendEmailAsync(WebConstants.EmailAdmin, subject, messageBody);

            InquiryHeader inquiryHeader = new InquiryHeader()
            {
                UserId = claim.Value,
                FullName = productUserViewModel.User.FullName,
                Email = productUserViewModel.User.Email,
                PhoneNumber = productUserViewModel.User.PhoneNumber,
                Address = productUserViewModel.User.Address,
                City = productUserViewModel.User.City,
                InquiryDate = DateTime.Now
            };

            _inquiryHeaderRepo.Add(inquiryHeader);
            _inquiryHeaderRepo.Save();

            foreach (var product in productUserViewModel.ProductList)
            {
                InquiryDetail inquiryDetail = new InquiryDetail()
                {
                    InquiryHeaderId = inquiryHeader.Id,
                    ProductId = product.Id
                };
                _inquiryDetailRepo.Add(inquiryDetail);

            }
            _inquiryDetailRepo.Save();
            return RedirectToAction(nameof(InquiaryConfirmation));
        }

        public IActionResult InquiaryConfirmation()
        {
            HttpContext.Session.Clear();

            return View();
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0)
            {
                //Session exsist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }
            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(IEnumerable<Product> productList)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            foreach (Product product in productList)
            {
                shoppingCartList.Add(new ShoppingCart { ProductId = product.Id, ByPeace = product.TempByPeace, ByWeight = product.TempByWeight });
            }

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    }
}
