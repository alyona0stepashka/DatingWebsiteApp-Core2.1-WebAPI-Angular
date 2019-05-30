using App.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IFriendService
    {
        Task<UserTabVM> CreateFriendRequestAsync(string user_id, string friend_id);
        Task<UserTabVM> DeleteFriendRequestAsync(string user_id, string friend_id);
        Task<UserTabVM> ConfirmFriendRequestAsync(string user_id, string friend_id);
        Task<UserTabVM> DeleteFriendAsync(string user_id, string friend_id);
        Task<List<UserTabVM>> GetIncomingsAsync(string user_id);
        Task<List<UserTabVM>> GetOutgoingsAsync(string user_id);
        Task<List<UserTabVM>> GetFriendsAsync(string user_id);
    }
}
