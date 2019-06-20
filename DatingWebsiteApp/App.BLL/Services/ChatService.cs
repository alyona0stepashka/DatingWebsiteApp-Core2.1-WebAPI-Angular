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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetChatIdByUsersAsync(string me_id, string friend_id)
        {
            try
            {
                var chat = _db.Chats.GetWhere(m=> (m.UserFromId==me_id && m.UserToId==friend_id) || (m.UserToId == me_id && m.UserFromId == friend_id)).FirstOrDefault();
                if (chat == null)
                {
                    throw new Exception("Chat Not Found");
                }
                return chat.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsChatExist(string caller_id, string receiver_id)
        {
            try
            {
                var chat = _db.Chats.GetWhere(m => (m.UserFromId == caller_id && m.UserToId == receiver_id) || (m.UserToId == caller_id && m.UserFromId == receiver_id)); 
                return chat.Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetChatReceiverIdAsync(int chat_id, string me_id)
        {
            try
            {
                var chat = await GetDbChatAsync(chat_id);
                if (chat == null)
                {
                    throw new Exception("Chat not found");
                }
                return (chat.UserToId==me_id) ? chat.UserFromId : chat.UserToId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ReadAllNewMessages(List<ChatMessage> messages)
        {
            try
            { 
                foreach (var mess in messages)
                {
                    mess.IsNew = false;
                    await _db.ChatMessages.UpdateAsync(mess);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChatTabVM>> GetChatListByUserIdAsync(string user_id)
        {
            try
            {
                var chat_list = new List<ChatTabVM>();
                var db_chats = _db.Chats.GetWhere(m => m.UserFromId == user_id || m.UserToId == user_id);
                if (db_chats == null)
                {
                    return chat_list;
                }
                foreach (var chat in db_chats)
                {
                    chat_list.Add(new ChatTabVM(chat, user_id));
                    await ReadAllNewMessages(chat.Messages.Where(m=>m.UserSenderId!=user_id).ToList());
                }
                return chat_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChatMessageVM>> GetChatByIdAsync(int chat_id, string user_id)
        {
            try
            {
                var message_list = new List<ChatMessageVM>();
                var db_chat = await GetDbChatAsync(chat_id);
                if (db_chat == null)
                {
                    throw new Exception("Chat not found");
                }
                var db_messages = db_chat.Messages.Where(m=>(db_chat.UserToId==user_id && m.DateSend>db_chat.UserToClearHistory) || (db_chat.UserFromId == user_id && m.DateSend > db_chat.UserFromClearHistory));
                if (db_messages == null)
                {
                    return message_list;
                }
                foreach (var msg in db_messages)
                {
                    message_list.Add(new ChatMessageVM(msg));
                }
                return message_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ChatRoom> CreateChatAsync(string me_id, string receiver_id)
        {
            try
            {
                var new_chat = await _db.Chats.CreateAsync(new ChatRoom
                {
                    IsBlock = false,
                    UserFromClearHistory = DateTime.Now,
                    UserFromId = me_id,
                    UserToId = receiver_id
                }); 
                return new_chat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChatMessage> SendMessageAsync(ChatMessageSendVM message, string me_id)
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
                //if (message.UploadFiles != null)
                //{
                //    foreach (var file in message.UploadFiles)
                //    {
                //        await _fileService.CreatePhotoForMessageAsync(file, new_message);
                //    }
                //}
                return new_message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ClearChatHistoryAsync(int chat_id, string me_id)
        {
            try
            {
                var chat = await _db.Chats.GetByIdAsync(chat_id);
                if (chat == null)
                {
                    throw new Exception("Chat not found");
                }
                if (chat.UserFromId == me_id)
                {
                    chat.UserFromClearHistory = DateTime.Now;
                }
                else
                {
                    chat.UserToClearHistory = DateTime.Now;
                }
                await _db.Chats.UpdateAsync(chat); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
