using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class UserService: IUserService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; 
        private readonly IFileService _fileService;  

        public UserService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signManager, 
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
            _signInManager = signManager; 
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
    }
}
