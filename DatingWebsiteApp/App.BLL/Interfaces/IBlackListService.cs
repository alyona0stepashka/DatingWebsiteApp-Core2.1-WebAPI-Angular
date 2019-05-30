using App.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IBlackListService
    {
        Task<UserTabVM> AddToBlackListAsync(string user_id, string bad_guy_id);
        Task<UserTabVM> DeleteFromBlackListAsync(string user_id, string bad_guy_id);
        Task<List<UserTabVM>> GetBlackListWithMeAsync(string user_id);
        Task<List<UserTabVM>> GetMyBlackListAsync(string user_id);
    }
}
