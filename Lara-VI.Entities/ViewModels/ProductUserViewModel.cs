using Lara_VI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Entities.ViewModels
{
    public class ProductUserViewModel
    {
        public ProductUserViewModel()
        {
            ProductList = new List<Product>();
        }
        public User User { get; set; }
        public IList<Product> ProductList { get; set; }
    }
}
