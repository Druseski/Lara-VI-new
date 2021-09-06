using Lara_VI.Data;
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

    public class GenericService<T> : IGenereicService<T> where T : class
    {
        private readonly IGenereicRepository<T>  _genereicRepository;
       

        public GenericService(
            IGenereicRepository<T> genereicRepository)
        {
            _genereicRepository = genereicRepository;
            
        }

        public void Add(T entity)
        {
            _genereicRepository.Add(entity);
        }

        public T Find(int id)
        {
            return _genereicRepository.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {

            return _genereicRepository.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            return _genereicRepository.GetAll();
        }

        public void Remove(T entity)
        {
            _genereicRepository.Remove(entity);
        }

        public void Save()
        {
            _genereicRepository.Save();
        }
    }
}
