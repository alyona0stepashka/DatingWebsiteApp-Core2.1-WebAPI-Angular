using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;

namespace App.BLL.ViewModels
{
    public class ChatMessageVM
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public string SenderId { get; set; }

        public string SenderAvatarPath { get; set; }

        public string SenderName { get; set; }

        public string Text { get; set; }

        public List<string> FilePathes { get; set; }

        public DateTime DateSend { get; set; }

        public bool IsNew { get; set; }

        public ChatMessageVM(ChatMessage message)
        {
            Id = message.Id;
            ChatId = message.ChatId;
            SenderAvatarPath = message.UserSender.File.Path;
            SenderId = message.UserSenderId;
            SenderName = message.UserSender.Name;
            Text = message.Text;
            FilePathes = new List<string>();
            if (message.Files != null)
            {
                foreach(var file in message.Files)
                {
                    FilePathes.Add(file.Path);
                }
            }
            DateSend = message.DateSend;
            IsNew = message.IsNew;
        }
    }
}
