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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public UserController(IAccountService accountService,
            IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        [Route("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userService.GetVMUserAsync(user_id);
            if (user == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(user);
        }


        [HttpGet("{id}")] 
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        { 
            var user = await _userService.GetVMUserAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditUserInformation([FromBody] UserInfoEditVM editUser)
        {
            if (editUser == null)
                return BadRequest(new { message = "editUser param is null." });

            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            editUser.Id = user_id;

            var user = await _userService.EditUserInfo(editUser);
            if (user == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditUserPhoto([FromBody] UserPhotoCreateVM editUser)
        {
            if (editUser == null)
                return BadRequest(new { message = "editUser param is null." });

            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            editUser.Id = user_id;

            var user = await _userService.EditUserPhoto(editUser);
            if (user == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(user);
        }
    }
}