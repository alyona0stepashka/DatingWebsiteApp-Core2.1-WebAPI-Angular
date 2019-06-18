using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/blacklist")]
    [ApiController]
    public class BlackListController : ControllerBase
    {
        private readonly IBlackListService _blackListService;

        public BlackListController(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }

        [HttpGet]
        [Route("outgoing")]
        [Authorize]
        public async Task<IActionResult> GetMyBlackList()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var black_list = await _blackListService.GetMyBlackListAsync(user_id);
                if (black_list == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(black_list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from BlackListController: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("incoming")]
        [Authorize]
        public async Task<IActionResult> GetBlackListWithMe()
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                var black_list = await _blackListService.GetBlackListWithMeAsync(user_id);
                if (black_list == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(black_list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from BlackListController: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> CreateBlack(string id)  //bad_guy_id
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                if (user_id == id)
                {
                    return BadRequest(new { message = "You cannot (user_id == bad_guy_id)." });
                }
                var new_bad = await _blackListService.AddToBlackListAsync(user_id, id);
                if (new_bad == null)
                {
                    return NotFound(new { message = "User not found by id (or user_in_black_list already exist)." });
                }
                return Ok(new_bad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from BlackListController: " + ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlack(string id)  //bad_guy_id
        {
            try
            {
                var user_id = User.Claims.First(c => c.Type == "UserID").Value;
                if (user_id == id)
                {
                    return BadRequest(new { message = "You cannot (user_id == bad_guy_id)." });
                }
                var new_good = await _blackListService.DeleteFromBlackListAsync(user_id, id);
                if (new_good == null)
                {
                    return NotFound(new { message = "User not found by id." });
                }
                return Ok(new_good);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Exception from BlackListController: " + ex.Message });
            }
        }
    }
}