using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Repositories.Utility;

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lara_VI.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if(obj == WebConstants.CategoryName)
            {
                return _dataContext.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
            }
            return null;
        }

        public void Update(Product  product)
        {
            var productDb = base.FirstOrDefault(u => u.Id == product.Id);
            if (productDb != null)
            {
                productDb.Name = product.Name;
                productDb.Price = product.Price;
                productDb.ShortDescription = product.Name;
                productDb.Description = product.Description;
                productDb.CategoryId = product.CategoryId;
                productDb.Category = product.Category;
                productDb.Image = product.Image;
                productDb.Popularity = product.Popularity;
                productDb.ByWeight = product.ByWeight;
                productDb.ByPeace = product.ByPeace;
            }
        }
    }
}
