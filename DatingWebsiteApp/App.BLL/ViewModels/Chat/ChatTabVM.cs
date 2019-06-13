using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;

namespace App.BLL.ViewModels.Chat
{
    public class ChatTabVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ChatIconPath { get; set; }

        public string LastSenderAvatarPath { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageDateTime { get; set; }

        public bool HasNew { get; set; }

        public ChatTabVM(ChatRoom chat, string me_id)
        {
            Id = chat.Id;
            if (me_id != null)
            {
                if (chat.UserFromId == me_id)
                {
                    Name = chat.UserTo.Name;
                    ChatIconPath = chat.UserTo.File.Path;
                }
                else
                {
                    Name = chat.UserFrom.Name;
                    ChatIconPath = chat.UserFrom.File.Path;
                }
            }
            var last = chat.Messages.LastOrDefault();
            LastSenderAvatarPath = last.UserSender.File.Path;
            LastMessage = last.Text;
            LastMessageDateTime = last.DateSend;
            HasNew = chat.Messages.Where(m => m.IsNew == true).Any();
        }
    }
}
