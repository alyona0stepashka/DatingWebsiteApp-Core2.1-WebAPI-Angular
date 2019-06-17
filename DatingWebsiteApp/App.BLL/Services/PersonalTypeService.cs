using App.BLL.Interfaces;
using App.BLL.ViewModels;
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

        public async Task<PersonalType> EditTypeAsync(PersonalType type, UserInfoEditVM model)
        {
            try
            {
                if (model.Growth != null) { type.Growth = model.Growth.Value; }
                if (model.Weight != null) { type.Weight = model.Weight.Value; }
                if (model.Education != null)
                {
                    //type.EducationId = (_db.Educations.GetWhere(m => m.Value == model.Education)).FirstOrDefault().Id;
                    type.EducationId = model.Education.Value;
                }
                if (model.Nationality != null)
                {
                    //type.NationalityId = (_db.Nationalities.GetWhere(m => m.Value == model.Nationality)).FirstOrDefault().Id;
                    type.NationalityId = model.Nationality;
                }
                if (model.Zodiac != null)
                {
                    //type.ZodiacId = (_db.Zodiacs.GetWhere(m => m.Value == model.Zodiac)).FirstOrDefault().Id;
                    type.ZodiacId = model.Zodiac.Value;
                }
                if (model.FinanceStatus != null)
                {
                    //type.FinanceStatusId = (_db.FinanceStatuses.GetWhere(m => m.Value == model.FinanceStatus)).FirstOrDefault().Id;
                    type.FinanceStatusId = model.FinanceStatus;
                }
                if (model.FamilyStatus != null)
                {
                    //type.FamilyStatusId = (_db.FamilyStatuses.GetWhere(m => m.Value == model.FamilyStatus)).FirstOrDefault().Id;
                    type.FamilyStatusId = model.FamilyStatus;
                }
                if (model.Weight != null)
                {
                    type.Weight = model.Weight.Value;
                }
                if (model.Growth != null)
                {
                    type.Growth = model.Growth.Value;
                }
                if (model.Languages != null)
                {
                    foreach (var db_item in type.Languages)
                    {
                        await _db.LanguageUsers.DeleteAsync(db_item.Id);
                    }
                    foreach (var new_item_id in model.Languages)
                    {
                        var bu = await _db.LanguageUsers.CreateAsync(new LanguageUser { LanguageId = new_item_id, PersonalTypeId = type.Id });
                        //type.Languages.Add(bu);
                    }
                    type.Languages = null;
                }
                if (model.BadHabits != null)
                {
                    //type.EducationId = (_db.Educations.GetWhere(m => m.Value == model.Education)).FirstOrDefault().Id;
                    foreach (var db_item in type.BadHabits)
                    {
                        await _db.BadHabitUsers.DeleteAsync(db_item.Id);
                    }
                    foreach (var new_item_id in model.BadHabits)
                    {
                        var bu = await _db.BadHabitUsers.CreateAsync(new BadHabitUser { BadHabitId = new_item_id, PersonalTypeId = type.Id });
                        //type.BadHabits.Add(bu);
                    }
                    type.BadHabits = null;
                }
                if (model.Interests != null)
                {
                    //type.EducationId = (_db.Educations.GetWhere(m => m.Value == model.Education)).FirstOrDefault().Id;
                    foreach (var db_item in type.Interests)
                    {
                        await _db.InterestUsers.DeleteAsync(db_item.Id);
                    }
                    foreach (var new_item_id in model.Interests)
                    {
                        var iu = await _db.InterestUsers.CreateAsync(new InterestUser { InterestId = new_item_id, PersonalTypeId = type.Id });
                        //type.Interests.Add(iu);
                    }
                    type.Interests = null;
                }
                var new_type = await _db.PersonalTypes.UpdateAsync(type);
                return type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
