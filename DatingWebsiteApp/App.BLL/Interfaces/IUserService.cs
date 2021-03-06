﻿using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserInfoShowVM> GetVMUserAsync(string user_id, string my_id);
        Task<ApplicationUser> GetDbUserAsync(string user_id);
        Task<UserInfoShowVM> EditUserPhoto(UserPhotoCreateVM model);
        Task<UserInfoShowVM> EditUserInfo(UserInfoEditVM model);
        Task AddProfileVisitAsync(string user_id, string my_id);
        //List<UserTabVM> GetAllUsers();

    }
}
