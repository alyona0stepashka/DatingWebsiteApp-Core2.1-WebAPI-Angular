using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.WebAPI.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IHubContext<ChatHub> _hub;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public IActionResult Get()
        {
            var message = "Hellooooooooo";
            _hub.Clients.All.SendAsync("transferchartdata", message);

            return Ok(new { Message = "Request Completed" });
        }


    }
}