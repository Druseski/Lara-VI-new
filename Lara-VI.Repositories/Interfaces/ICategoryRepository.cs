
using Lara_VI.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenereicRepository<Category>
    {
        void Update(Category category);
    }
}
