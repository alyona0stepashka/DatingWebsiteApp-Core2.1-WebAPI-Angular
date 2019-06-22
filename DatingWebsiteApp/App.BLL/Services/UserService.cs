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

        public async Task<UserInfoShowVM> GetVMUserAsync(string user_id, string my_id)
        {
            try
            {
                var db_user = await GetDbUserAsync(user_id); 
                var user = new UserInfoShowVM(db_user, my_id);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddProfileVisitAsync(string user_id, string my_id)
        {
            try
            {
                var my_last_visit = _db.ProfileVisitors.GetWhere(m => m.OwnerProfileId == user_id && m.VisitorId == my_id &&
                                                                      m.LastVisit.Month == DateTime.Now.Month &&
                                                                      m.LastVisit.Year == DateTime.Now.Year).FirstOrDefault();
                if (my_last_visit == null)
                {
                    await _db.ProfileVisitors.CreateAsync(new ProfileVisitor { LastVisit = DateTime.Now, VisitorId = my_id, OwnerProfileId = user_id }); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplicationUser> GetDbUserAsync(string user_id)
        {
            try
            {
                var db_user = await _userManager.FindByIdAsync(user_id);
                if (db_user == null)
                {
                    throw new Exception("User not found");
                }
                return db_user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserInfoShowVM> EditUserInfo(UserInfoEditVM model)
        {
            try
            {
                var db_user = await GetDbUserAsync(model.Id); 
                if (model.DateBirth != null) { db_user.DateBirth = model.DateBirth.Value; }
                if (model.Name != null) { db_user.Name = model.Name; }
                if (model.MainGoal != null)
                { 
                    db_user.MainGoalId = model.MainGoal.Value;
                }
                db_user.MainGoal = null;
                if (model.IsAnonimus != null) { db_user.IsAnonimus = model.IsAnonimus.Value; }
                if (model.Sex != null)
                { 
                    db_user.SexId = model.Sex.Value;
                }
                db_user.Sex = null;
                //if (db_user.Type != null)
                //{ 
                if (model.BadHabits != null || model.Interests != null ||
                    model.Languages != null || model.Growth != null ||
                    model.Weight != null || model.FamilyStatus != null ||
                    model.FinanceStatus != null || model.Zodiac != null ||
                    model.Nationality != null || model.Education != null ||
                    model.Weight != null || model.Growth != null)
                {
                    db_user.Type = await _personalTypeService.EditTypeAsync(db_user.Type, model);
                   
                }
                //} 
                db_user.Type = null;
                db_user.File = null; 
                await _userManager.UpdateAsync(db_user);
                var user = new UserInfoShowVM(db_user, null);
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<UserInfoShowVM> EditUserPhoto(UserPhotoCreateVM model)
        {
            try
            {
                var db_user = await GetDbUserAsync(model.Id);
                db_user.FileId = await _fileService.CreatePhotoAsync(model.UploadPhoto);
                await _userManager.UpdateAsync(db_user);
                var user = new UserInfoShowVM(db_user, null);
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
