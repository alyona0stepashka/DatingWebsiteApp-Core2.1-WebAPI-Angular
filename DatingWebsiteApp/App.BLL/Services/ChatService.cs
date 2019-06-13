using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Services
{
    public class ChatService : IChatService
    {
        private IUnitOfWork _db { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;

        public ChatService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IFileService fileService)
        {
            _db = uow;
            _userManager = userManager;
            _fileService = fileService;
        }

        public async Task<ChatRoom> GetDbChatAsync(int chat_id)
        {
            try
            {
                var chat = await _db.Chats.GetByIdAsync(chat_id);
                return chat;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ChatTabVM> GetChatListByUserId(string user_id)
        {
            try
            {
                var chat_list = new List<ChatTabVM>();
                var db_chats = _db.Chats.GetWhere(m => m.UserFromId == user_id || m.UserToId == user_id);
                foreach (var chat in db_chats)
                {
                    chat_list.Add(new ChatTabVM(chat, user_id));
                }
                return chat_list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<ChatMessageVM>> GetChatByIdAsync(int chat_id, string user_id)
        {
            try
            {
                var message_list = new List<ChatMessageVM>();
                var db_chat = await GetDbChatAsync(chat_id);
                var db_messages = db_chat.Messages.Where(m=>(db_chat.UserToId==user_id && m.DateSend>db_chat.UserToClearHistory) || (db_chat.UserFromId == user_id && m.DateSend > db_chat.UserFromClearHistory)); 
                foreach (var msg in db_messages)
                {
                    message_list.Add(new ChatMessageVM(msg));
                }
                return message_list;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int?> SendMessageAsync(ChatMessageSendVM message, string me_id)
        {
            try
            {
                 var new_message = await _db.ChatMessages.CreateAsync(new ChatMessage
                 {
                     ChatId = message.ChatId,
                     DateSend = DateTime.Now,
                     UserSenderId = me_id,
                     IsNew = true,
                     Text = message.Text 
                 });
                if (message.UploadFiles != null)
                {
                    foreach (var file in message.UploadFiles)
                    {
                        await _fileService.CreatePhotoForMessageAsync(file, new_message);
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int?> ClearChatHistoryAsync(int chat_id, string me_id)
        {
            try
            {
                var chat = await _db.Chats.GetByIdAsync(chat_id);
                if (chat.UserFromId == me_id)
                {
                    chat.UserFromClearHistory = DateTime.Now;
                }
                else
                {
                    chat.UserToClearHistory = DateTime.Now;
                }
                await _db.Chats.UpdateAsync(chat);
                return 0;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
