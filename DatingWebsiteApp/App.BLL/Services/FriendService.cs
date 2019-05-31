using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class FriendService : IFriendService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public FriendService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _db = uow;
            _userManager = userManager;
            _userService = userService;
        }

        public Friendship GetFriendshipFor(string user_id, string friend_id)
        {
            var friendship = _db.Friendships.GetWhere(m => (m.UserFromId == user_id && m.UserToId == friend_id) || (m.UserFromId == friend_id && m.UserToId == user_id));
            if (friendship == null)
            {
                return null;
            }
            var one_friend = friendship.FirstOrDefault();
            if (one_friend == null)
            {
                return null;
            }
            return one_friend;
        }

        public async Task<List<UserTabVM>> GetIncomingsAsync(string user_id)
        {
            var me = await _userService.GetDbUserAsync(user_id); 
            var friendships = me.FriendshipsTo.Where(m=>m.Status==false);
            if (friendships == null)
            {
                return null;
            }
            var requests = new List<UserTabVM>();
            foreach(var friend in friendships)
            {
                requests.Add(new UserTabVM(friend.UserFrom));
            }
            return requests;
        }

        public async Task<List<UserTabVM>> GetOutgoingsAsync(string user_id)
        {
            var me = await _userService.GetDbUserAsync(user_id);
            var friendships = me.FriendshipsFrom.Where(m => m.Status == false);
            if (friendships == null)
            {
                return null;
            }
            var requests = new List<UserTabVM>();
            foreach (var friend in friendships)
            {
                requests.Add(new UserTabVM(friend.UserTo));
            }
            return requests;
        }
        public async Task<List<UserTabVM>> GetFriendsAsync(string user_id)
        {
            var me = await _userService.GetDbUserAsync(user_id);
            var friendshipsFrom = me.FriendshipsFrom.Where(m => m.Status == true);
            if (friendshipsFrom == null)
            {
                return null;
            }
            var friend_list = new List<UserTabVM>();
            foreach (var friend in friendshipsFrom)
            {
                friend_list.Add(new UserTabVM(friend.UserTo));
            }

            var friendshipsTo = me.FriendshipsTo.Where(m => m.Status == true);
            if (friendshipsTo == null)
            {
                return null;
            } 
            foreach (var friend in friendshipsTo)
            {
                friend_list.Add(new UserTabVM(friend.UserTo));
            }
            return friend_list;
        }

        public async Task<UserTabVM> CreateFriendRequestAsync(string user_id, string friend_id)
        {
            var friend = await _userService.GetDbUserAsync(friend_id);
            if (friend == null)
            {
                return null;
            }
            var is_exist = GetFriendshipFor(user_id, friend_id);
            if (is_exist != null)
            {
                return null;
            }
            await _db.Friendships.CreateAsync(new Friendship
            {
                UserFromId = user_id,
                UserToId = friend_id,
                Status = false
            });
            var new_friend = new UserTabVM(friend);
            return new_friend;
        }

        public async Task<UserTabVM> DeleteFriendRequestAsync(string user_id, string friend_id)
        {
            var friend = await _userService.GetDbUserAsync(friend_id);
            if (friend == null)
            {
                return null;
            }
            var friendship = GetFriendshipFor(user_id, friend_id); 
            if (friendship==null)
            {
                return null;
            }
            await _db.Friendships.DeleteAsync(friendship.Id);
            var new_friend = new UserTabVM(friend);
            return new_friend;
        }

        public async Task<UserTabVM> ConfirmFriendRequestAsync(string user_id, string friend_id)
        {
            var friend = await _userService.GetDbUserAsync(friend_id);
            if (friend == null)
            {
                return null;
            }
            var friendship = GetFriendshipFor(user_id, friend_id);
            if (friendship == null)
            {
                return null;
            }
            if (friendship.Status)
            {
                return null;
            }
            friendship.Status = true;
            await _db.Friendships.UpdateAsync(friendship);
            var new_friend = new UserTabVM(friend);
            return new_friend;
        }
        public async Task<UserTabVM> DeleteFriendAsync(string user_id, string friend_id)
        {
            var friend = await _userService.GetDbUserAsync(friend_id);
            if (friend == null)
            {
                return null;
            }
            var friendship = GetFriendshipFor(user_id, friend_id);
            if (friendship == null)
            {
                return null;
            }
            friendship.UserFromId = friend_id;
            friendship.UserToId = user_id;
            friendship.Status = false;
            await _db.Friendships.UpdateAsync(friendship);
            var old_friend = new UserTabVM(friend);
            return old_friend;
        }
    }
}
