using App.BLL.Infrastructure;
using App.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<string> RegisterUserAsync(RegisterVM model, string url);
        Task<OperationDetails> ConfirmEmailAsync(string user_id, string code);
        Task<object> LoginUserAsync(LoginVM model);
        Task EditAccountInfo(UserAccountInfoEditVM model);
        //Task<UserShowVM> GetUserAsync(string user_id);
        //Task<ApplicationUser> GetDbUserAsync(string user_id);
        void Dispose();
    }
}
