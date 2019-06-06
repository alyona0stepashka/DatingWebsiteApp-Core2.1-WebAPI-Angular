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
    [Route("api/static")]
    [ApiController]
    public class StaticController : ControllerBase
    {
        private readonly IStaticService _staticService; 

        public StaticController(IStaticService staticService )
        {
            _staticService = staticService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var statics = _staticService.GetAll();
            if (statics == null)
            {
                return NotFound(new { message = "Statics not found (error from service)." });
            }
            return Ok(statics);
        }
    }
}