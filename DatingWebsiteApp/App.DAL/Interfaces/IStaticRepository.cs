using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IStaticRepository<TEntity> 
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity item);
        //Task<TEntity> UpdateAsync(TEntity item);
        //Task<TEntity> DeleteAsync(TKey id);
    }
}