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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<object> Register([FromBody]RegisterVM model)
        {
            try
            {
                var url = HttpContext.Request.Host.ToString();
                var result = await _accountService.RegisterUserAsync(model, url);
                if (result == null)
                    return BadRequest(new { message = "Error by register." });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from AccountController: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginVM model)
        {
            try
            {
                var token = await _accountService.LoginUserAsync(model);
                if (token != null)
                    return Ok(new { token });
                return BadRequest(new { message = "Username or password is incorrect or not confirm email." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from AccountController: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> ConfirmEmail(string user_id, string code)  //user_id
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user_id) || string.IsNullOrWhiteSpace(code))
                {
                    ModelState.AddModelError("", "UserId and Code are required");
                    return BadRequest(ModelState);
                }
                var user = await _userService.GetDbUserAsync(user_id);
                if (user == null)
                {
                    return BadRequest("Error by confirm email.");
                }
                await _accountService.ConfirmEmailAsync(user_id, code);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from AccountController: " + ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditMyAccountInformation([FromBody] UserAccountInfoEditVM editUser)
        {
            try
            {
                if (editUser == null)
                    return BadRequest(new { message = "editUser param is null." });

                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                editUser.Id = user_id;

                var user = await _accountService.EditAccountInfo(editUser);
                if (user == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from AccountController: " + ex.Message });
            }
        }

    }
}