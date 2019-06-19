using App.BLL.Infrastructure;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Chat
{
    public class ChatHub : Hub
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg"};
        public static List<Connect> connects = new List<Connect>();
        private readonly IChatService _chatService;
        private readonly IFileService _fileService; 
        public ChatHub(IChatService chatService, 
                        IFileService fileService)
        {
            _chatService = chatService; 
            _fileService = fileService;
        } 
        public async override Task OnConnectedAsync()
        {
            try
            {
                var callerId = Context.User.Claims.First(c => c.Type == "UserID").Value; 
                UpdateList(callerId);
                await base.OnConnectedAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async override Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                connects.Remove(connects.Find(m => m.ConnectionId == Context.ConnectionId));
                await base.OnDisconnectedAsync(ex);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void UpdateList(string caller_id)
        {
            try
            {
                var index = connects.FindIndex(m => m.UserId == caller_id);
                if (index !=-1 && connects[index].ConnectionId != Context.ConnectionId)
                {
                    connects[index].ConnectionId = Context.ConnectionId;
                }
                else
                {
                    connects.Add(new Connect { ConnectionId = Context.ConnectionId, UserId = caller_id });
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        async Task AttachFilesAsync(ChatMessage db_message)
        {
            try
            { 
                var UploadFiles = Context.GetHttpContext().Request.Form.Files;
                foreach (var UploadPhoto in UploadFiles)
                {
                    if (UploadPhoto.Length != 0)
                    {
                        if (UploadPhoto.Length <= 2 * 1024 * 1024)
                        {
                            if (ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(UploadPhoto.FileName).ToLower()))
                            {
                                await _fileService.CreatePhotoForMessageAsync(UploadPhoto, db_message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        void FindCallerReceiver(string receiver_id, out Connect caller, out Connect receiver)
        {
            try
            {
                receiver = connects.Find(m => m.UserId == receiver_id);
                caller = connects.Find(m => m.ConnectionId == Context.ConnectionId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task Send (ChatMessageSendVM message) //(ChatMessageSendVM message)
        {
            try
            {
                Connect receiver, caller;
                var receiver_id = await _chatService.GetChatReceiverIdAsync(message.ChatId, Context.User.Claims.First(c => c.Type == "UserID").Value);
                FindCallerReceiver(receiver_id, out caller, out receiver); 
                var db_message = await _chatService.SendMessageAsync(message, caller.UserId);
                await AttachFilesAsync(db_message);
                await Clients.Client(caller.ConnectionId).SendAsync("SendMyself", new ChatMessageVM(db_message), caller.UserId);
                if (receiver != null)
                {
                    await Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message), caller.UserId);
                    //await Clients.Client(receiver.ConnectionId).SendAsync("SoundNotify", "/sounds/message.mp3");
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task SendFromProfile(ChatMessageSendVM message) //(ChatMessageSendVM message)
        {
            try
            {
                Connect receiver, caller; 
                FindCallerReceiver(message.ReceiverId, out caller, out receiver);
                var isChatExist = _chatService.IsChatExist(caller.UserId, message.ReceiverId);
                if (isChatExist)
                {
                    await _chatService.CreateChatAsync(caller.UserId, message.ReceiverId); 
                    //await Clients.Client(receiver.ConnectionId).SendAsync("SoundNotify");
                } 
                var db_message = await _chatService.SendMessageAsync(message, caller.UserId);
                await AttachFilesAsync(db_message);
                if (receiver != null)
                {
                    await Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message), caller.UserId);
                    //await Clients.Client(receiver.ConnectionId).SendAsync("SoundNotify", "/sounds/message.mp3");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
