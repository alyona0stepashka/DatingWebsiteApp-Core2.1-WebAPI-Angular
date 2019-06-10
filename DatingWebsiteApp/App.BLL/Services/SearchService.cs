﻿using App.BLL.Interfaces;
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
            if (search.Education != null)
            {
                foreach(var item in search.Education)
                {
                    if ((await _db.Educations.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.Type.EducationId == item);
                    }
                }
            }
            if (search.FamilyStatus != null)
            {
                foreach (var item in search.Education)
                {
                    if ((await _db.FamilyStatuses.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.Type.FamilyStatusId == item);
                    }
                } 
            }
            if (search.FinanceStatus != null) 
            {
                foreach (var item in search.FinanceStatus)
                {
                    if ((await _db.FinanceStatuses.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.Type.FinanceStatusId == item);
                    }
                } 
            }
            //if ((search.Interest != null) && ((await _db.Interests.GetByIdAsync(search.Interest.Value)).Value != "Not Defined"))
            //{
            //    user_list = user_list.Where(m => m.Type.InterestId == search.Interest);
            //}
            //if ((search.Language != null) && ((await _db.Languages.GetByIdAsync(search.Language.Value)).Value != "Not Defined"))
            //{
            //    user_list = user_list.Where(m => m.Type.LanguageId == search.Language);
            //}
            if (search.MainGoal != null) 
            {
                foreach (var item in search.MainGoal)
                {
                    if ((await _db.MainGoals.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.MainGoalId == item);
                    }
                } 
            }
            if (search.Nationality != null)
            {
                foreach (var item in search.Nationality)
                {
                    if ((await _db.MainGoals.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.Type.NationalityId == item);
                    }
                } 
            }
            if (search.Zodiac != null) 
            {
                foreach (var item in search.Nationality)
                {
                    if ((await _db.Zodiacs.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.Type.ZodiacId == item);
                    }
                } 
            }
            if (search.Sex != null)
            {
                foreach (var item in search.Sex)
                {
                    if ((await _db.Sexes.GetByIdAsync(item)).Value != "Not Defined")
                    {
                        user_list = user_list.Where(m => m.SexId == item);
                    }
                } 
            } 
            foreach (var user in user_list)
            {
                ret_list.Add(new UserTabVM(user));
            }
            return ret_list;
        }
    }
}
