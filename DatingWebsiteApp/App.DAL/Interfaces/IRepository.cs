using App.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IRepository<TEntity>: IStaticRepository<TEntity> where TEntity: EntityBase
    {
        //Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<TEntity> DeleteAsync(int id);
    }
}