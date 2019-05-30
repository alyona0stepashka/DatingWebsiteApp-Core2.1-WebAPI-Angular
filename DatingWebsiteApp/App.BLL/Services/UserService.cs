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
    public class UserService: IUserService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IFileService _fileService;  

        public UserService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager, 
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager; 
            _fileService = fileService;
        }


        public async Task<UserShowVM> GetVMUserAsync(string user_id)
        {
            var db_user = await GetDbUserAsync(user_id);
            if (db_user == null)
            {
                return null;
            } 
            var user = new UserShowVM(db_user);
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

        public async Task<UserShowVM> EditUserInfo(UserInfoEditVM model)
        {
            var db_user = await GetDbUserAsync(model.Id);
            if (db_user == null)
            {
                return null;
            } 

            if (model.Name != null) { db_user.Name = model.Name; }
            if (model.MainGoal != null) { db_user.MainGoal = model.MainGoal; }
            if (model.IsAnonimus != null) { db_user.IsAnonimus = model.IsAnonimus.Value; }
            if (model.Sex != null)
            {
                db_user.SexId = (_db.Sexes.GetWhere(m => m.Value == model.Sex)).FirstOrDefault().Id;
            }

            if (model.Growth != null) { db_user.Type.Growth = model.Growth.Value; }
            if (model.Weight != null) { db_user.Type.Weight = model.Weight.Value; }
            if (model.Education != null)
            {
                db_user.Type.EducationId = (_db.Educations.GetWhere(m => m.Value == model.Education)).FirstOrDefault().Id;
            }
            if (model.Nationality != null)
            {
                db_user.Type.NationalityId = (_db.Nationalities.GetWhere(m => m.Value == model.Nationality)).FirstOrDefault().Id;
            }
            if (model.Zodiac != null)
            {
                db_user.Type.ZodiacId = (_db.Zodiacs.GetWhere(m => m.Value == model.Zodiac)).FirstOrDefault().Id;
            }
            if (model.FinanceStatus != null)
            {
                db_user.Type.FinanceStatusId = (_db.FinanceStatuses.GetWhere(m => m.Value == model.FinanceStatus)).FirstOrDefault().Id;
            }
            if (model.FamilyStatus != null)
            {
                db_user.Type.FamilyStatusId = (_db.FamilyStatuses.GetWhere(m => m.Value == model.FamilyStatus)).FirstOrDefault().Id;
            }
            await _db.PersonalTypes.UpdateAsync(db_user.Type);
            await _userManager.UpdateAsync(db_user);
            var user = new UserShowVM(db_user);
            return user;
        }
        public async Task<UserShowVM> EditUserPhoto(UserPhotoCreateVM model)
        {
            var db_user = await GetDbUserAsync(model.Id);
            if (db_user == null)
            {
                return null;
            }
            db_user.FileId = await _fileService.CreatePhotoAsync(model.UploadPhoto);
            await _userManager.UpdateAsync(db_user);
            var user = new UserShowVM(db_user);
            return user;
        }
    }
}
