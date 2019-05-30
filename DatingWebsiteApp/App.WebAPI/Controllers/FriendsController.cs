using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFriends()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var request_friends = await _friendService.GetFriendsAsync(user_id);
            if (request_friends == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(request_friends);
        }

        [HttpGet]
        [Route("incoming")]
        [Authorize]
        public async Task<IActionResult> GetIncomingRequests () 
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var request_friends = await _friendService.GetIncomingsAsync(user_id);
            if (request_friends == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(request_friends);
        }

        [HttpGet]
        [Route("outgoing")]
        [Authorize]
        public async Task<IActionResult> GetOutgoingRequests ()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var request_friends = await _friendService.GetOutgoingsAsync(user_id);
            if (request_friends == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(request_friends);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> CreateRequest (string id)  //friend_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var new_friend = await _friendService.CreateFriendRequestAsync(user_id, id);
            if (new_friend == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(new_friend);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRequest(string id)  //friend_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var friend = await _friendService.DeleteFriendRequestAsync(user_id, id);
            if (friend == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(friend);
        }

        [HttpGet("{id}")]
        [Route("confirmation")]
        [Authorize]
        public async Task<IActionResult> ConfirmRequest(string id)  //friend_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var friend = await _friendService.ConfirmFriendRequestAsync(user_id, id);
            if (friend == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(friend);
        }

        [HttpGet("{id}")]
        [Route("delete")]  //??? name route
        [Authorize]
        public async Task<IActionResult> DeleteFriend(string id)  //friend_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var friend = await _friendService.DeleteFriendAsync(user_id, id);
            if (friend == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(friend);
        }

    }
}