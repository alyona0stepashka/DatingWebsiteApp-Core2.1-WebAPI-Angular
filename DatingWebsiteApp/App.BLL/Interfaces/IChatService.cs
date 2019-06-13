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
        List<ChatTabVM> GetChatListByUserId(string user_id);
        Task<List<ChatMessageVM>> GetChatByIdAsync(int chat_id, string user_id);
        Task<int?> SendMessageAsync(ChatMessageSendVM message, string me_id);
        Task<int?> ClearChatHistoryAsync(int chat_id, string me_id);
    }
}
