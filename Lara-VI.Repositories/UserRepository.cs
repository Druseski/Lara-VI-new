using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext):base(dataContext)
        {
            _dataContext = dataContext;
        }

        
    }
}
