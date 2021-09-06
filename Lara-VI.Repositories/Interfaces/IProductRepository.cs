using Lara_VI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Repositories.Interfaces
{
    public interface IProductRepository : IGenereicRepository<Product>
    {
        void Update(Product product);
        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
