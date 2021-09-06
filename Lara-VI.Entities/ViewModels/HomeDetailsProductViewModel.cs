using Lara_VI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Entities.ViewModels
{
    public class HomeDetailsProductViewModel
    {
        public HomeDetailsProductViewModel()
        {
            Product = new Product();
          
        }
      
        public Product Product { get; set; }
        public bool ExistInCart { get; set; }

    }
}
