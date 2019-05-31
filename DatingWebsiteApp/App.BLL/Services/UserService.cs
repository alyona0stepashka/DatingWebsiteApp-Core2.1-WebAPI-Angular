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
    public class UserService: IUserService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IFileService _fileService;
        private readonly IPersonalTypeService _personalTypeService;

        public UserService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IPersonalTypeService personalTypeService,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager; 
            _fileService = fileService;
            _personalTypeService = personalTypeService;
        }

        public async Task<UserInfoShowVM> GetVMUserAsync(string user_id)
        {
            var db_user = await GetDbUserAsync(user_id);
            if (db_user == null)
            {
                return null;
            } 
            var user = new UserInfoShowVM(db_user);
            return user;
        }

        public async Task<ApplicationUser> GetDbUserAsync(string user_id)
        {
            var db_user = await _userManager.FindByIdAsync(user_id);
            if (db_user == null)
            {
                return null;
            }
            return db_user;
        }

        public async Task<UserInfoShowVM> EditUserInfo(UserInfoEditVM model)
        {
            var db_user = await GetDbUserAsync(model.Id);
            if (db_user == null)
            {
                return null;
            }
            if (model.DateBirth != null) { db_user.DateBirth = model.DateBirth; }
            if (model.Name != null) { db_user.Name = model.Name; }
            if (model.MainGoal != null) { db_user.MainGoal = model.MainGoal; }
            if (model.IsAnonimus != null) { db_user.IsAnonimus = model.IsAnonimus.Value; }
            if (model.Sex != null)
            {
                db_user.SexId = (_db.Sexes.GetWhere(m => m.Value == model.Sex)).FirstOrDefault().Id;
            }
            if (db_user.Type != null)
            {
                await _personalTypeService.EditTypeAsync(db_user.Type, model);
            }
            else
            {
                db_user.Type = new PersonalType();
            }
            await _userManager.UpdateAsync(db_user);
            var user = new UserInfoShowVM(db_user);
            return user;
        }
        public async Task<UserInfoShowVM> EditUserPhoto(UserPhotoCreateVM model)
        {
            var db_user = await GetDbUserAsync(model.Id);
            if (db_user == null)
            {
                return null;
            }
            db_user.FileId = await _fileService.CreatePhotoAsync(model.UploadPhoto);
            await _userManager.UpdateAsync(db_user);
            var user = new UserInfoShowVM(db_user);
            return user;
        }
    }
}