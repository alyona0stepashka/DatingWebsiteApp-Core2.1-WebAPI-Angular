using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IRepository<TEntity,TKey>: IStaticRepository<TEntity,TKey> where TEntity:class
    {
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<TEntity> DeleteAsync(TKey id);
    }
}