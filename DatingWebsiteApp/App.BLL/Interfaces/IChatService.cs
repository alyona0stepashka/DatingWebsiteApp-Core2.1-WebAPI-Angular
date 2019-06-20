using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Interfaces
{
    public interface IChatService
    {
        Task<ChatRoom> GetDbChatAsync(int chat_id);
        Task<List<ChatTabVM>> GetChatListByUserIdAsync(string user_id);
        Task<List<ChatMessageVM>> GetChatByIdAsync(int chat_id, string user_id);
        Task<ChatMessage> SendMessageAsync(ChatMessageSendVM message, string me_id);
        Task ClearChatHistoryAsync(int chat_id, string me_id);
        bool IsChatExist(string caller_id, string receiver_id);
        Task<ChatRoom> CreateChatAsync(string me_id, string receiver_id);
        Task<string> GetChatReceiverIdAsync(int chat_id, string me_id);
        int GetChatIdByUsersAsync(string me_id, string friend_id);
        Task ReadAllNewMessages(List<ChatMessage> messages);
    }
}
