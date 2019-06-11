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
    public class BlackListService : IBlackListService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        public BlackListService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IFriendService friendService)
        {
            _db = uow;
            _userManager = userManager;
            _userService = userService;
            _friendService = friendService;
        }

        public BlackList GetBlackFor(string user_id, string bad_guy_id)
        {
            var black = _db.BlackLists.GetWhere(m => (m.UserFromId == user_id && m.UserToId == bad_guy_id));
            if (black == null)
            {
                return null;
            }
            var one_black = black.FirstOrDefault();
            if (one_black == null)
            {
                return null;
            }
            return one_black;
        }

        public async Task<List<UserTabVM>> GetMyBlackListAsync(string user_id)
        {
            var me = await _userService.GetDbUserAsync(user_id);
            if (me == null)
            {
                return null;
            }
            var black_list = me.BlackListsFrom;
            var ret_list = new List<UserTabVM>();
            foreach(var black in black_list)
            {
                ret_list.Add(new UserTabVM(black.UserTo));
            }
            return ret_list;
        }

        public async Task<List<UserTabVM>> GetBlackListWithMeAsync(string user_id)
        {
            var me = await _userService.GetDbUserAsync(user_id);
            if (me == null)
            {
                return null;
            }
            var black_list = me.BlackListsTo;
            var ret_list = new List<UserTabVM>();
            foreach (var black in black_list)
            {
                ret_list.Add(new UserTabVM(black.UserFrom));
            }
            return ret_list;
        }

        public async Task<UserTabVM> AddToBlackListAsync(string user_id, string bad_guy_id)
        {
            var bad_guy = await _userService.GetDbUserAsync(bad_guy_id);
            if (bad_guy == null)
            {
                return null;
            }
            var is_exist = GetBlackFor(user_id, bad_guy_id);
            if (is_exist != null)
            {
                return null;
            }
            await _db.BlackLists.CreateAsync(new BlackList
            {
                UserFromId=user_id,
                UserToId=bad_guy_id
            });
            await _friendService.DeleteFriendAsync(user_id, bad_guy_id);
            var new_bad = new UserTabVM(bad_guy);
            return new_bad;
        }

        public async Task<UserTabVM> DeleteFromBlackListAsync(string user_id, string bad_guy_id)
        {
            var bad_guy = await _userService.GetDbUserAsync(bad_guy_id);
            if (bad_guy == null)
            {
                return null;
            }
            var black = GetBlackFor(user_id, bad_guy_id);
            await _db.BlackLists.DeleteAsync(black.Id);
            var new_good = new UserTabVM(bad_guy);
            return new_good;
        }
    }
}
