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
        public IActionResult FirstSearch()
        {
            var search_result_list = _userService.GetAllUsers();
            if (search_result_list == null)
            {
                return NotFound(new { message = "Users not found (error from service)." });
            }
            return Ok(search_result_list);
        }

        [HttpPost]
        [Authorize]
        public IActionResult StartSearch([FromBody]SearchVM model)
        {
            var search_result_list = _searchService.StartSearchAsync(model);
            if (search_result_list == null)
            {
                return NotFound(new { message = "Users not found (error from service)." });
            }
            return Ok(search_result_list);
        }

    }
}