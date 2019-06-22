using App.BLL.Chat;
using App.BLL.Infrastructure;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ChatHub> _hub;

        public ChatService(IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IFileService fileService, 
            IHubContext<ChatHub> hub)
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
        public async Task SendSignalRService(ChatMessageSendVM message, string me_id)
        {
            try
            {
                Connect receiver = null;
                var caller = ChatHub.connects.Find(m => m.UserId == me_id);
                ChatMessage db_message = new ChatMessage();                

                if (message.ReceiverId != "0" && message.ChatId == 0)
                {                    
                    var isChatExist = IsChatExist(me_id, message.ReceiverId);
                    message.ChatId = !isChatExist ? (await CreateChatAsync(me_id, message.ReceiverId)).Id
                                                  : GetChatIdByUsersAsync(me_id, message.ReceiverId); 
                    db_message = await SendMessageAsync(message, me_id); 
                }
                else if (message.ReceiverId == "0" && message.ChatId != 0)
                { 
                    db_message = await SendMessageAsync(message, me_id);  
                    message.ReceiverId = await GetChatReceiverIdAsync(message.ChatId, me_id); 
                }
                receiver = ChatHub.connects.Find(m => m.UserId == message.ReceiverId);
                db_message = await _db.ChatMessages.GetByIdAsync(db_message.Id);
                await _hub.Clients.Client(caller.ConnectionId).SendAsync("SendMyself", new ChatMessageVM(db_message)); 
                if (receiver != null)
                {
                    await _hub.Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message));
                }

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

        public List<ChatTabVM> GetChatListByUserId(string user_id)
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
                await ReadAllNewMessages(db_chat.Messages.Where(m => m.UserSenderId != user_id).ToList());
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
                foreach (var UploadPhoto in message.UploadFiles)
                {
                    _fileService.IsValidFile(UploadPhoto, 2);
                    await _fileService.CreatePhotoForMessageAsync(UploadPhoto, new_message);
                } 
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
