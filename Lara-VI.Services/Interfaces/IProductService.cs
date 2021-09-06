using Lara_VI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Services.Interfaces
{
    public interface IProductService : IGenereicService<Product>
    {
        void Update(Product product);
        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
