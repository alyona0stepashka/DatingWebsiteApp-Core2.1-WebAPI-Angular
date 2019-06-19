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

        public StaticController(IStaticService staticService)
        {
            _staticService = staticService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var statics = _staticService.GetAll();
                if (statics == null)
                {
                    throw new Exception("Statics not found (error from service).");
                }
                return Ok(statics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}