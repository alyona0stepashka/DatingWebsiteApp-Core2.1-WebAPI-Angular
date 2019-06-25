using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;
using Newtonsoft.Json;

namespace App.BLL.ViewModels
{
    public class ChatMessageVM
    {
        [JsonProperty(nameof(Id))]
        public int Id { get; set; }
        [JsonProperty(nameof(ChatId))]

        public int ChatId { get; set; }
        [JsonProperty(nameof(SenderId))]

        public string SenderId { get; set; }
        [JsonProperty(nameof(SenderAvatarPath))]

        public string SenderAvatarPath { get; set; }
        [JsonProperty(nameof(SenderName))]

        public string SenderName { get; set; }
        [JsonProperty(nameof(Text))]

        public string Text { get; set; }
        [JsonProperty(nameof(FilePathes))]

        public List<string> FilePathes { get; set; }
        [JsonProperty(nameof(DateSend))]

        public DateTime DateSend { get; set; }
        [JsonProperty(nameof(IsNew))]

        public bool IsNew { get; set; }

        public ChatMessageVM(ChatMessage message)
        {
            Id = message.Id;
            ChatId = message.ChatId;
            SenderAvatarPath = message.UserSender.File.Path;
            SenderId = message.UserSenderId;
            SenderName = message.UserSender.Name;
            Text = message.Text;
            //Text = message.Text.Replace("\n", "<br/>");
            //Text = message.Text.Replace("\n", "&#10;");
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
