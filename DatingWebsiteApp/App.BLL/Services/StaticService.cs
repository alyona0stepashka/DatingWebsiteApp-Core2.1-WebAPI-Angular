using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class StaticService : IStaticService
    {
        private IUnitOfWork _db { get; set; } 
        public StaticService(IUnitOfWork uow )
        {
            _db = uow; 
        }

        public StaticAllVM GetAll()
        {
            var retVal = new StaticAllVM();
            retVal.FinanceStatuses = _db.FinanceStatuses.GetAll().ToList();
            retVal.FamilyStatuses = _db.FamilyStatuses.GetAll().ToList();
            retVal.Nationalities = _db.Nationalities.GetAll().ToList();
            retVal.Educations = _db.Educations.GetAll().ToList();
            retVal.MainGoals = _db.MainGoals.GetAll().ToList();
            retVal.Languages = _db.Languages.GetAll().ToList();
            retVal.Interests = _db.Interests.GetAll().ToList();
            retVal.BadHabits = _db.BadHabits.GetAll().ToList();
            retVal.Zodiacs = _db.Zodiacs.GetAll().ToList();
            retVal.Sexes = _db.Sexes.GetAll().ToList();
            return retVal;
        }
    }
}
