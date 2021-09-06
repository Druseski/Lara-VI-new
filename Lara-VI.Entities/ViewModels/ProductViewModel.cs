using Lara_VI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lara_VI.Entities.ViewModels
{
    public class ProductViewModel
    {
        public Product  Product { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
