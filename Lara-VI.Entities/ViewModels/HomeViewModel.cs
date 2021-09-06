using Lara_VI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Entities.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
