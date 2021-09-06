using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lara_VI.Services
{
    public class CategoryService :GenericService<Category> ,ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository
           ):base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }

        
        public void Update(Category category)
        {
            _categoryRepository.Update(category);
            
        }
    }
}
