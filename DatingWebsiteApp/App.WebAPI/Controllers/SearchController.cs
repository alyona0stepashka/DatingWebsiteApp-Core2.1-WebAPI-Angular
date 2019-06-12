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
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IUserService _userService;

        public SearchController(ISearchService searchService,
            IUserService userService)
        {
            _searchService = searchService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FirstSearch()
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var search_result_list = await _searchService.StartSearchAsync(new SearchVM(), user_id);
            if (search_result_list == null)
            {
                return NotFound(new { message = "Users not found (error from service)." });
            }
            return Ok(search_result_list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StartSearch([FromBody]SearchVM model)
        {
            var user_id = User.Claims.First(c => c.Type == "UserID").Value;
            var search_result_list = await _searchService.StartSearchAsync(model, user_id);
            if (search_result_list == null)
            {
                return NotFound(new { message = "Users not found (error from service)." });
            }
            return Ok(search_result_list);
        }

    }
}