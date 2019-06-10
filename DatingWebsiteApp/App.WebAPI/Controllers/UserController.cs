using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
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
        public async Task<IActionResult> GetMyUserProfile()
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
        public async Task<IActionResult> EditMyUserInformation([FromBody] UserInfoEditVM editUser)
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
        public async Task<IActionResult> EditMyUserPhoto([FromBody] UserPhotoCreateVM editUser)
        {
            //if (editUser == null)
            //    return BadRequest(new { message = "editUser param is null." });

            //var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            //editUser.Id = user_id;

            //var user = await _userService.EditUserPhoto(editUser);
            //if (user == null)
            //{
            //    return NotFound(new { message = "User not found by id." });
            //}
            //return Ok(user);

            if (editUser.UploadPhoto == null) return BadRequest("Null File");
            if (editUser.UploadPhoto.Length == 0)
            {
                return BadRequest("Empty File");
            }
            if (editUser.UploadPhoto.Length > 10 * 1024 * 1024) return BadRequest("Max file size exceeded.");
            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(editUser.UploadPhoto.FileName).ToLower())) return BadRequest("Invalid file type.");

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