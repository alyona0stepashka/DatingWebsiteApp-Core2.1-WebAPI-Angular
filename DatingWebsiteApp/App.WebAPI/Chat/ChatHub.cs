using App.BLL.Infrastructure;
using App.BLL.Interfaces;
using App.BLL.ViewModels;
using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Chat2
{
    public class ChatHub : Hub
    { 
        public static List<Connect> connects = new List<Connect>();
        private readonly IChatService _chatService;
        private readonly IFileService _fileService;
        private readonly IFriendService _friendService;
        public ChatHub(IChatService chatService, 
                        IFileService fileService,
                        IFriendService friendService)
        {
            _chatService = chatService; 
            _fileService = fileService;
            _friendService = friendService;
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
        async Task AttachFilesAsync(ChatMessage db_message, IFormFileCollection UploadFiles)
        {
            try
            {  
                foreach (var UploadPhoto in UploadFiles)
                {
                    _fileService.IsValidFile(UploadPhoto, 2);
                    await _fileService.CreatePhotoForMessageAsync(UploadPhoto, db_message); 
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
        public async Task Send(/*IFormCollection formData*/ChatMessageSendVM message)
        {
            try
            {
                Connect receiver, caller;
                //var message = new ChatMessageSendVM { ChatId = Convert.ToInt32(formData["ChatId"]), ReceiverId = formData["ReceiverId"], Text = formData["Text"], UploadFiles = formData.Files };
                var receiver_id = await _chatService.GetChatReceiverIdAsync(message.ChatId, Context.User.Claims.First(c => c.Type == "UserID").Value);
                FindCallerReceiver(receiver_id, out caller, out receiver);
                var db_message = await _chatService.SendMessageAsync(message, caller.UserId);
                //if (message.UploadFiles != null && message.UploadFiles.Count > 0)
                //{
                //    await AttachFilesAsync(db_message, message.UploadFiles);
                //}
                await Clients.Client(caller.ConnectionId).SendAsync("SendMyself", new ChatMessageVM(db_message), caller.UserId);
                if (receiver != null)
                {
                    await Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message), caller.UserId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task SendFromProfile(/*IFormCollection formData*/ChatMessageSendVM message) 
        {
            try
            {
                Connect receiver, caller;
                //var message = new ChatMessageSendVM { ChatId = Convert.ToInt32(formData["ChatId"]), ReceiverId = formData["ReceiverId"], Text = formData["Text"], UploadFiles = formData.Files };
                FindCallerReceiver(message.ReceiverId, out caller, out receiver);
                var isChatExist = _chatService.IsChatExist(caller.UserId, message.ReceiverId);
                message.ChatId = !isChatExist ? (await _chatService.CreateChatAsync(caller.UserId, message.ReceiverId)).Id
                                             : _chatService.GetChatIdByUsersAsync(caller.UserId, message.ReceiverId); 

                var db_message = await _chatService.SendMessageAsync(message, caller.UserId);
                //if (message.UploadFiles != null && message.UploadFiles.Count > 0)
                //{
                //    await AttachFilesAsync(db_message, message.UploadFiles);
                //}
                if (receiver != null)
                {
                    await Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message), caller.UserId); 
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //public async Task SendFriendRequest(/*IFormCollection formData*/string sender_id, string receiver_id)
        //{
        //    try
        //    {
        //        Connect receiver, caller; 
        //        FindCallerReceiver(receiver_id, out caller, out receiver);
        //        var db_message = await _chatService.SendMessageAsync(message, caller.UserId);
        //        //if (message.UploadFiles != null && message.UploadFiles.Count > 0)
        //        //{
        //        //    await AttachFilesAsync(db_message, message.UploadFiles);
        //        //}
        //        await Clients.Client(caller.ConnectionId).SendAsync("SendMyself", new ChatMessageVM(db_message), caller.UserId);
        //        if (receiver != null)
        //        {
        //            await Clients.Client(receiver.ConnectionId).SendAsync("Send", new ChatMessageVM(db_message), caller.UserId);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
