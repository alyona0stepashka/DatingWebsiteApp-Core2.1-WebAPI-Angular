using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetDbAll();
        IQueryable<TEntity> GetDbWhere(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetDbByIdAsync(int id);
        Task<TEntity> CreateDbAsync(TEntity entity);
        Task<TEntity> UpdateDbAsync(TEntity entity);
        Task<TEntity> DeleteDbAsync(int id);

    }
}
