using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using App.BLL.Chat;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.WebAPI.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;
        private readonly IHubContext<ChatHub> _hub;

        public FriendsController(IFriendService friendService, IHubContext<ChatHub> hub)
        {
            _friendService = friendService;
            _hub = hub;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFriends()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var request_friends = await _friendService.GetFriendsAsync(user_id);
                if (request_friends == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(request_friends);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("incoming")]
        [Authorize]
        public async Task<IActionResult> GetIncomingRequests()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var request_friends = await _friendService.GetIncomingsAsync(user_id);
                if (request_friends == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(request_friends);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("outgoing")]
        [Authorize]
        public async Task<IActionResult> GetOutgoingRequests()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var request_friends = await _friendService.GetOutgoingsAsync(user_id);
                if (request_friends == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(request_friends);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("request/{id}")]
        [Authorize]
        public async Task<IActionResult> CreateRequest(string id)  //friend_id
        {
            try
            {
                var me_id = User.Claims.First(c => c.Type == "UserID").Value;
                if (me_id == id)
                {
                    return BadRequest(new { message = "You cannot (me_id == friend_id)." });
                }
                var new_friend = await _friendService.CreateFriendRequestAsync(me_id, id);
                if (new_friend == null)
                {
                    return NotFound(new { message = "User not found by id (or user_in_friend_list already exist)." });
                }
                var connect = ChatHub.connects.Find(m => m.UserId == new_friend.Id);
                if (connect != null)
                {
                    await _hub.Clients.Client(connect.ConnectionId).SendAsync("SendFriendRequest", new_friend.Name + ", " + new_friend.Age);
                }
                return Ok(new_friend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete]
        //[Route("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteRequest(string id)  //friend_id
        //{
        //    var user_id = User.Claims.First(c => c.Type == "UserID").Value;
        //    if (user_id == id)
        //    {
        //        return BadRequest(new { message = "You cannot (user_id == friend_id)." });
        //    }
        //    var friend = await _friendService.DeleteFriendRequestAsync(user_id, id);
        //    if (friend == null)
        //    {
        //        return NotFound(new { message = "User not found by id." });
        //    }
        //    return Ok(friend);
        //}

        [HttpGet]
        [Route("confirmation/{id}")]
        [Authorize]
        public async Task<IActionResult> ConfirmRequest(string id)  //friend_id
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                if (user_id == id)
                {
                    return BadRequest(new { message = "You cannot (user_id == friend_id)." });
                }
                var friend = await _friendService.ConfirmFriendRequestAsync(user_id, id);
                if (friend == null)
                {
                    return NotFound(new { message = "User not found by id (or this user is already being your friend)." });
                }
                return Ok(friend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]  //??? name route
        [Authorize]
        public async Task<IActionResult> DeleteFriend(string id)  //friend_id
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                if (user_id == id)
                {
                    throw new Exception("You cannot (user_id == friend_id).");
                }
                var friend = await _friendService.DeleteFriendAsync(user_id, id);
                if (friend == null)
                {
                    throw new Exception("User not found by id." );
                }
                return Ok(friend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}