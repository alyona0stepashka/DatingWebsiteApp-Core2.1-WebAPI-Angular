using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.Chat;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace App.WebAPI.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("my")]
        [Authorize]
        public async Task<IActionResult> GetMyChatRooms()
        {
            try
            {
                var me_id = User.Claims.First(c => c.Type == "UserID").Value;
                var chat_list = await _chatService.GetChatListByUserIdAsync(me_id);
                if (chat_list == null)
                {
                    throw new Exception("Chat not found by id." );
                }
                return Ok(chat_list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetChatByIdAsync(int id)
        {
            try
            {
                var me_id = User.Claims.First(c => c.Type == "UserID").Value;
                var chat_list = await _chatService.GetChatByIdAsync(id, me_id);
                if (chat_list == null)
                {
                    throw new Exception("Chat not found by id.");
                }
                return Ok(chat_list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("clear/{id}")]
        [Authorize]
        public async Task<IActionResult> ClearChatHistoryByIdAsync(int id)
        {
            try
            {
                var me_id = User.Claims.First(c => c.Type == "UserID").Value;
                await _chatService.ClearChatHistoryAsync(id, me_id); 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}