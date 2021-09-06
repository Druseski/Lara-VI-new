using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Services.Interfaces;
using Lara_VI.Repositories.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lara_VI.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository
           ) : base(productRepository)
        {
            _productRepository = productRepository;

        }

        public void Update(Product product)
        {
            _productRepository.Update(product);

        }
        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            return _productRepository.GetAllDropdownList(obj);
        }

    }


}
