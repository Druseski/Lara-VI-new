using Lara_VI.Entities;
using Lara_VI.Entities.ViewModels;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class InquiryController : Controller
    {
        private readonly IInquiryHeaderRepository _inquiryHeaderRepo;
        private readonly IInquiryDetailRepository _inquiryDetailRepo;
        [BindProperty]
        public InquiryViewModel InquiryViewModel { get; set; }

        public InquiryController(IInquiryHeaderRepository inquiryHeaderRepo,
            IInquiryDetailRepository inquiryDetailRepo)
        {
            _inquiryHeaderRepo = inquiryHeaderRepo;
            _inquiryDetailRepo = inquiryDetailRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            InquiryViewModel = new InquiryViewModel()
            {
                InquiryHeader = _inquiryHeaderRepo.FirstOrDefault(u => u.Id == id),
                InquiryDetail = _inquiryDetailRepo.GetAll(u => u.InquiryHeaderId == id,includeProperties:"Product")
            };
            return View(InquiryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            InquiryViewModel.InquiryDetail = _inquiryDetailRepo.GetAll(u => u.InquiryHeaderId == InquiryViewModel.InquiryHeader.Id);

            foreach(var detail in InquiryViewModel.InquiryDetail)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    ProductId = detail.ProductId
                };
                shoppingCartList.Add(shoppingCart);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartList);
            HttpContext.Session.Set(WebConstants.SessionInquiryId, InquiryViewModel.InquiryHeader.Id);

            return RedirectToAction("Index","ShoppingCart");
        }

        [HttpPost]
        public IActionResult Delete()
        {
            InquiryHeader inquiryHeader = _inquiryHeaderRepo.FirstOrDefault(u => u.Id == InquiryViewModel.InquiryHeader.Id);
            IEnumerable<InquiryDetail> inquiryDetails = _inquiryDetailRepo.GetAll(u=>u.InquiryHeaderId == InquiryViewModel.InquiryHeader.Id);

            _inquiryDetailRepo.RemoveRange(inquiryDetails);
            _inquiryHeaderRepo.Remove(inquiryHeader);
            _inquiryHeaderRepo.Save();
            return RedirectToAction(nameof(Index));
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = _inquiryHeaderRepo.GetAll() });
        }
        #endregion
    }
}
