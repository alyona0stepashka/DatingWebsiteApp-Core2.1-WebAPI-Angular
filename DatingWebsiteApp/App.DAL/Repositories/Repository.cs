using System;
using Microsoft.EntityFrameworkCore;
using App.DAL.Data;
using App.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using App.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace App.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:EntityBase  //StaticRepository<TEntity,TKey> where TEntity:class ???
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext context)
        {
            _db = context;
        } 
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _db.Set<TEntity>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            await _db.SaveChangesAsync();
            return _db.Set<TEntity>().AsNoTracking();
        }

        public async Task<IQueryable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _db.SaveChangesAsync();
            return _db.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }  
    }
}
