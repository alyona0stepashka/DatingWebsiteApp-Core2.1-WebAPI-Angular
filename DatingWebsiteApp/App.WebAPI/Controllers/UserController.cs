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
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userService.GetVMUserAsync(user_id, null);
                if (user == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from UserController: " + ex.Message });
            }
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userService.GetVMUserAsync(id, user_id);
                if (user == null)
                {
                    throw new Exception("UserNotFound.");
                   // return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from UserController: " + ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditMyUserInformation([FromBody] UserInfoEditVM editUser)
        {
            try
            {
                if (editUser == null)
                    return BadRequest(new { error_message = "editUser param is null." });

                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                editUser.Id = user_id;

                var user = await _userService.EditUserInfo(editUser);
                if (user == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from UserController: " + ex.Message });
            }
        }

        [HttpPut, Route("avatar")]
        [Authorize]
        public async Task<IActionResult> EditMyUserPhoto()
        {
            try
            {
                var UploadPhoto = HttpContext.Request.Form.Files[0];

                if (UploadPhoto == null) return BadRequest("Null File");
                if (UploadPhoto.Length == 0)
                {
                    return BadRequest("Empty File");
                }
                if (UploadPhoto.Length > 5 * 1024 * 1024) return BadRequest("Max file size exceeded.");
                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(UploadPhoto.FileName).ToLower())) return BadRequest("Invalid file type.");

                var user_id = User.Claims.First(c => c.Type == "UserID").Value;

                var editUser = new UserPhotoCreateVM() { Id = user_id, UploadPhoto = UploadPhoto };
                editUser.Id = user_id;

                var user = await _userService.EditUserPhoto(editUser);
                if (user == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from UserController: " + ex.Message });
            }

        }

    }
}