using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IService<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetDbAllAsync();
        Task<IEnumerable<TEntity>> GetDbWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetDbByIdAsync(TKey id);
        Task<TEntity> AddDb(TEntity entity);
        Task<TEntity> UpdateDb(TEntity entity);
        Task<TEntity> DeleteDb(TEntity entity);

    }
}
