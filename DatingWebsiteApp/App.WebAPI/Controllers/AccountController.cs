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
                var user_id = await _accountService.RegisterUserAsync(model, url);
                if (user_id == null)
                {
                    throw new Exception("Register fail");
                }                    
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                {
                    return Ok(new { token });
                } 
                throw new Exception("Username or password is incorrect or not confirm email.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    //ModelState.AddModelError("", );
                    throw new Exception("UserId and Code are required");
                } 
                await _accountService.ConfirmEmailAsync(user_id, code);
                return Redirect("http://localhost:4200/auth/login");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditMyAccountInformation([FromBody] UserAccountInfoEditVM editUser)
        {
            try
            {
                if (editUser == null)
                {
                    throw new Exception("editUser param is null");
                }  
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                editUser.Id = user_id;
                await _accountService.EditAccountInfo(editUser);
                //var user = await _accountService.EditAccountInfo(editUser);
                //if (user == null)
                //{
                //    throw new Exception("User not found");
                //}
                //return Ok(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}