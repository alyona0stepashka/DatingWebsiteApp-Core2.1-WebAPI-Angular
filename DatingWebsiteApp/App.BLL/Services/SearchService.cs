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

        public List<UserTabVM> StartSearch(SearchVM search)  //??? add filter by online/offline
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
            if (search.FamilyStatus != null)
            {
                user_list = user_list.Where(m => m.Type.FamilyStatus.Value==search.FamilyStatus);
            }
            if (search.MainGoal != null)
            {
                user_list = user_list.Where(m => m.MainGoal == search.MainGoal);
            }
            if (search.Sex != null)
            {
                user_list = user_list.Where(m => m.Sex.Value == search.Sex);
            } 
            foreach (var user in user_list)
            {
                ret_list.Add(new UserTabVM(user));
            }
            return ret_list;
        }
    }
}
