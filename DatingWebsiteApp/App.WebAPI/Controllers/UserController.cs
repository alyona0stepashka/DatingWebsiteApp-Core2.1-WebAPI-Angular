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
        private readonly IFileService _fileService;

        public UserController(IAccountService accountService,
            IUserService userService,
            IFileService fileService)
        {
            _accountService = accountService;
            _userService = userService;
            _fileService = fileService;
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
                    throw new Exception("User not found by id.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(string id)
        {
            try
            {
                var me_id = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userService.GetVMUserAsync(id, me_id);
                await _userService.AddProfileVisitAsync(id, me_id);
                if (user == null)
                {
                    throw new Exception("UserNotFound.");
                   // return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditMyUserInformation([FromBody] UserInfoEditVM editUser)
        {
            try
            {
                if (editUser == null)
                {
                    throw new Exception("editUser param is null.");
            }                    

                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                editUser.Id = user_id;

                var user = await _userService.EditUserInfo(editUser);
                if (user == null)
                {
                    throw new Exception("User not found by id.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("avatar")]
        [Authorize]
        public async Task<IActionResult> EditMyUserPhoto()
        {
            try
            {
                var UploadPhoto = HttpContext.Request.Form.Files[0];

                if (UploadPhoto == null)
                {
                    throw new Exception("Null photo");
                }

                _fileService.IsValidFile(UploadPhoto, 5);

                var user_id = User.Claims.First(c => c.Type == "UserID").Value;

                var editUser = new UserPhotoCreateVM() { Id = user_id, UploadPhoto = UploadPhoto };
                editUser.Id = user_id;

                var user = await _userService.EditUserPhoto(editUser);
                if (user == null)
                {
                    throw new Exception("User not found by id.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}