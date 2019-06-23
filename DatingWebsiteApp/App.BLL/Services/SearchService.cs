using App.BLL.Chat;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class SearchService : ISearchService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager)
        {
            _db = uow;
            _userManager = userManager;
        }

        public async Task<List<UserTabVM>> StartSearchAsync(SearchVM search, string my_id)  //??? add filter by online/offline
        {
            try
            {
                var cur_user = await _userManager.FindByIdAsync(my_id);
                if (cur_user == null)
                {
                    throw new Exception("User not found");
                }
                var buffer_int = new List<int>();
                var now_year = DateTime.Now.Year;
                var ret_list = new List<UserTabVM>();
                var user_list = _userManager.Users./*AsNoTracking().*/Where(m => m.IsAnonimus == false && m.Id != my_id && m.DateBirth>new DateTime(1900,1,1));

                if (search.NetworkStatus != null)
                {
                    var conn = ChatHub.connects.Select(m=>m.UserId);
                    user_list = search.NetworkStatus.Value ? user_list.Where(m => conn.Contains(m.Id))
                                                           : user_list.Where(m => !conn.Contains(m.Id));
                }

                if (search.AgeFrom != null)
                {
                    user_list = user_list.Where(m => (now_year - m.DateBirth.Year >= search.AgeFrom));
                }

                if (search.AgeTo != null)
                {
                    user_list = user_list.Where(m => (now_year - m.DateBirth.Year <= search.AgeTo));
                } 

                if (search.Education != null && search.Education.Count>0)
                {
                    foreach (var item in search.Education)
                    {
                        if ((await _db.Educations.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.Type.EducationId != null &&
                                                             search.Education.Contains(u.Type.EducationId.Value));
                        }
                    }
                }

                if (search.FamilyStatus != null && search.FamilyStatus.Count > 0)
                {
                    foreach (var item in search.FamilyStatus)
                    {
                        if ((await _db.FamilyStatuses.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list
                                .Where(u => u.Type.FamilyStatusId != null &&
                                      search.FamilyStatus.Contains(u.Type.FamilyStatusId.Value));
                        }
                    }
                }

                if (search.FinanceStatus != null && search.FinanceStatus.Count > 0)
                {
                    foreach (var item in search.FinanceStatus)
                    {
                        if ((await _db.FinanceStatuses.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.Type.FinanceStatusId != null &&
                                                             search.FinanceStatus.Contains(u.Type.FinanceStatusId.Value));
                        }
                    }
                } 

                if (search.MainGoal != null && search.MainGoal.Count > 0)
                {
                    foreach (var item in search.MainGoal)
                    {
                        if ((await _db.MainGoals.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.MainGoalId != null &&
                                                             search.MainGoal.Contains(u.MainGoalId.Value));
                        }
                    }
                }

                if (search.Nationality != null && search.Nationality.Count > 0)
                {
                    foreach (var item in search.Nationality)
                    {
                        if ((await _db.MainGoals.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.Type.NationalityId != null &&
                                                             search.Nationality.Contains(u.Type.NationalityId.Value));
                        }
                    }
                }

                if (search.Zodiac != null && search.Zodiac.Count > 0)
                {
                    foreach (var item in search.Zodiac)
                    {
                        if ((await _db.Zodiacs.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.Type.ZodiacId != null &&
                                                             search.Zodiac.Contains(u.Type.ZodiacId.Value));
                        }
                    }
                }

                if (search.Sex != null && search.Sex.Count > 0)
                {
                    foreach (var item in search.Sex)
                    {
                        if ((await _db.Sexes.GetByIdAsync(item)).Value != "Not Defined")
                        {
                            user_list = user_list.Where(u => u.SexId != null &&
                                                             search.Sex.Contains(u.SexId.Value));
                        }
                    }
                }

                if (search.Interest != null && search.Interest.Count > 0)
                {
                    user_list = user_list.Where(user =>
                        user.Type.Interests
                            .Select(interest => interest.InterestId)
                            .Intersect(search.Interest)
                            .Any());
                }

                if (search.BadHabit != null && search.BadHabit.Count > 0)
                {
                    user_list = user_list.Where(user =>
                        user.Type.BadHabits
                            .Select(hab => hab.BadHabitId)
                            .Intersect(search.BadHabit)
                            .Any());
                }

                if (search.Language != null && search.Language.Count > 0)
                {
                    user_list = user_list.Where(user =>
                        user.Type.Languages
                            .Select(lang => lang.LanguageId)
                            .Intersect(search.Language)
                            .Any());
                } 

                foreach (var user in user_list)
                {
                    ret_list.Add(new UserTabVM(user));
                }
                return ret_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
