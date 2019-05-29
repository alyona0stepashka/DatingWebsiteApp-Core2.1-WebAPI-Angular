using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserShowVM> GetVMUserAsync(string user_id);
        Task<ApplicationUser> GetDbUserAsync(string user_id);

    }
}
