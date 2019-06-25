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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: EntityBase  /*StaticRepository<TEntity> where TEntity:EntityBase*/ 
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IQueryable<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _db.Set<TEntity>()
                 //.AsNoTracking()
                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        { 
            return _db.Set<TEntity>()/*.AsNoTracking()*/;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        { 
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
