using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lara_VI.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext):base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void Update(Category category)
        {
            var categoryDb = base.FirstOrDefault(u =>u.Id == category.Id);
            if(categoryDb != null)
            {
                categoryDb.Name = category.Name;
                
            }
        }
    }
}
