using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class SearchService: ISearchService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;  

        public SearchService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager )
        {
            _db = uow;
            _userManager = userManager; 
        }

        public async Task<List<UserTabVM>> StartSearchAsync(SearchVM search)  //??? add filter by online/offline
        {
            var now_year = DateTime.Now.Year;
            var ret_list = new List<UserTabVM>();
            var user_list = _userManager.Users.Where(m=>m.IsAnonimus==false);
            if (search.AgeFrom != null)
            {
                user_list = user_list.Where(m => (now_year - m.DateBirth.Year >= search.AgeFrom));
            }
            if (search.AgeTo != null)
            {
                user_list = user_list.Where(m => (now_year - m.DateBirth.Year <= search.AgeTo));
            }
            //var bad_hab = (await _db.BadHabits.GetByIdAsync(search.BadHabit.Value));
            //if ((search.BadHabit != null) && (bad_hab.Value != "Not Defined"))
            //{
            //    user_list = user_list.Where(m => m.Type.BadHabits.Contains(bad_hab);
            //}
            if ((search.Education != null) && ((await _db.Educations.GetByIdAsync(search.Education.Value)).Value != "Not Defined"))
            {
                user_list = user_list.Where(m => m.Type.EducationId == search.Education);
            }
            if ((search.FamilyStatus != null) && ((await _db.FamilyStatuses.GetByIdAsync(search.FamilyStatus.Value)).Value!="Not Defined"))
            {
                user_list = user_list.Where(m => m.Type.FamilyStatusId==search.FamilyStatus);
            }
            if ((search.FinanceStatus != null) && ((await _db.FinanceStatuses.GetByIdAsync(search.Education.Value)).Value != "Not Defined"))
            {
                user_list = user_list.Where(m => m.Type.FinanceStatusId == search.FinanceStatus);
            }
            //if ((search.Interest != null) && ((await _db.Interests.GetByIdAsync(search.Interest.Value)).Value != "Not Defined"))
            //{
            //    user_list = user_list.Where(m => m.Type.InterestId == search.Interest);
            //}
            //if ((search.Language != null) && ((await _db.Languages.GetByIdAsync(search.Language.Value)).Value != "Not Defined"))
            //{
            //    user_list = user_list.Where(m => m.Type.LanguageId == search.Language);
            //}
            if ((search.MainGoal != null) && ((await _db.MainGoals.GetByIdAsync(search.MainGoal.Value)).Value != "Not Defined"))
            {
                user_list = user_list.Where(m => m.MainGoal.Id == search.MainGoal);
            }
            if ((search.Nationality != null) && ((await _db.Nationalities.GetByIdAsync(search.Nationality.Value)).Value != "Not Defined"))
            {
                user_list = user_list.Where(m => m.Type.NationalityId == search.Nationality);
            }
            if ((search.Zodiac != null) && ((await _db.Zodiacs.GetByIdAsync(search.Zodiac.Value)).Value != "Not Defined"))
            {
                user_list = user_list.Where(m => m.Type.ZodiacId == search.Zodiac);
            }
            if (search.Sex != null)
            {
                user_list = user_list.Where(m => m.Sex.Id == search.Sex);
            } 
            foreach (var user in user_list)
            {
                ret_list.Add(new UserTabVM(user));
            }
            return ret_list;
        }
    }
}
