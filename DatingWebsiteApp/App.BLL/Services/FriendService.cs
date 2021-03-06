﻿using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class FriendService : IFriendService
    {
        private IUnitOfWork _db { get; set; } 
        private readonly IUserService _userService;

        public FriendService(IUnitOfWork uow, 
            IUserService userService)
        {
            _db = uow; 
            _userService = userService;
        }

        public Friendship GetFriendshipFor(string user_id, string friend_id)
        {
            try
            {
                var friendship = _db.Friendships.GetWhere(m => (m.UserFromId == user_id && m.UserToId == friend_id) || (m.UserFromId == friend_id && m.UserToId == user_id)).FirstOrDefault();
                if (friendship == null)
                {
                    return null;
                } 
                return friendship;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserTabVM>> GetIncomingsAsync(string user_id)
        {
            try
            {
                var requests = new List<UserTabVM>();
                var me = await _userService.GetDbUserAsync(user_id); 
                var friendships = me.FriendshipsTo.Where(m => m.Status == false);
                if (friendships != null)
                {
                    foreach (var friend in friendships)
                    {
                        requests.Add(new UserTabVM(friend.UserFrom));
                    }
                }
                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserTabVM>> GetOutgoingsAsync(string user_id)
        {
            try
            {
                var requests = new List<UserTabVM>();
                var me = await _userService.GetDbUserAsync(user_id); 
                var friendships = me.FriendshipsFrom.Where(m => m.Status == false);
                if (friendships != null)
                {
                    foreach (var friend in friendships)
                    {
                        requests.Add(new UserTabVM(friend.UserTo));
                    }
                }
                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<UserTabVM>> GetFriendsAsync(string user_id)
        {
            try
            {
                var friend_list = new List<UserTabVM>();
                var me = await _userService.GetDbUserAsync(user_id); 
                if (me.FriendshipsFrom != null)
                {
                    var friendshipsFrom = me.FriendshipsFrom.Where(m => m.Status == true);
                    foreach (var friend in friendshipsFrom)
                    {
                        friend_list.Add(new UserTabVM(friend.UserTo));
                    }
                }
                if (me.FriendshipsTo != null)
                {
                    var friendshipsTo = me.FriendshipsTo.Where(m => m.Status == true);
                    foreach (var friend in friendshipsTo)
                    {
                        friend_list.Add(new UserTabVM(friend.UserFrom));
                    }
                }
                return friend_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserTabVM> CreateFriendRequestAsync(string user_id, string friend_id)
        {
            try
            {
                var friend = await _userService.GetDbUserAsync(friend_id); 
                var friendship = GetFriendshipFor(user_id, friend_id);
                if (friendship != null)
                {
                    throw new Exception("User already is your friend");
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserTabVM> DeleteFriendRequestAsync(string user_id, string friend_id)
        {
            try
            {
                var friend = await _userService.GetDbUserAsync(friend_id); 
                var friendship = GetFriendshipFor(user_id, friend_id);
                if (friendship == null)
                {
                    throw new Exception("User not your friend");
                }
                await _db.Friendships.DeleteAsync(friendship.Id);
                var new_friend = new UserTabVM(friend);
                return new_friend;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserTabVM> ConfirmFriendRequestAsync(string user_id, string friend_id)
        {
            try
            {
                var friend = await _userService.GetDbUserAsync(friend_id); 
                var friendship = GetFriendshipFor(user_id, friend_id);
                if (friendship == null)
                {
                    throw new Exception("Friend request not found");
                }
                if (friendship.Status)
                {
                    throw new Exception("User already your friend");
                }
                friendship.Status = true;
                await _db.Friendships.UpdateAsync(friendship);
                var new_friend = new UserTabVM(friend);
                return new_friend;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserTabVM> DeleteFriendAsync(string user_id, string friend_id)
        {
            try { 
            var friend = await _userService.GetDbUserAsync(friend_id); 
            var friendship = GetFriendshipFor(user_id, friend_id);
            if (friendship == null)
            {
                    throw new Exception("User not your friend");
            }
            if (friendship.Status)
            {
                friendship.UserFromId = friend_id;
                friendship.UserToId = user_id;
                friendship.Status = false;
                await _db.Friendships.UpdateAsync(friendship);
            }
            else
            {
                await _db.Friendships.DeleteAsync(friendship.Id);
            }
            var old_friend = new UserTabVM(friend);
            return old_friend;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
