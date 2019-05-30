using App.BLL.Interfaces;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class PersonalTypeService : IPersonalTypeService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager; 

        public PersonalTypeService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager )
        {
            _db = uow;
            _userManager = userManager; 
        }
        public async Task<PersonalType> CreateDbAsync(PersonalType entity)
        {
            return await _db.PersonalTypes.CreateAsync(entity);
        }

        public async Task<PersonalType> DeleteDbAsync(int id)
        {
            return await _db.PersonalTypes.DeleteAsync(id);
        }

        public IQueryable<PersonalType> GetDbAll()
        {
            return _db.PersonalTypes.GetAll();
        }

        public async Task<PersonalType> GetDbByIdAsync(int id)
        {
            return await _db.PersonalTypes.GetByIdAsync(id);
        }

        public IQueryable<PersonalType> GetDbWhere(Expression<Func<PersonalType, bool>> predicate)
        {
            return _db.PersonalTypes.GetWhere(predicate);
        }

        public async Task<PersonalType> UpdateDbAsync(PersonalType entity)
        {
            return await _db.PersonalTypes.UpdateAsync(entity);
        }
    }
}
