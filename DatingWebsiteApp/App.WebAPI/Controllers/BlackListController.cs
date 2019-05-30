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
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var black_list = await _blackListService.GetMyBlackListAsync(user_id);
            if (black_list == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(black_list);
        }

        [HttpGet]
        [Route("incoming")]
        [Authorize]
        public async Task<IActionResult> GetBlackListWithMe()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var black_list = await _blackListService.GetMyBlackListAsync(user_id);
            if (black_list == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(black_list);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> CreateBlack(string id)  //bad_guy_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var new_bad = await _blackListService.AddToBlackListAsync(user_id, id);
            if (new_bad == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(new_bad);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlack(string id)  //bad_guy_id
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var new_good = await _blackListService.DeleteFromBlackListAsync(user_id, id);
            if (new_good == null)
            {
                return NotFound(new { message = "User not found by id." });
            }
            return Ok(new_good);
        }
    }
}