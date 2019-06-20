using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;

namespace App.BLL.ViewModels
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

        public bool IsBlock { get; set; }

        public ChatTabVM(ChatRoom chat, string me_id)
        {
            Id = chat.Id;
            IsBlock = false;
            HasNew = false;
            if (!String.IsNullOrEmpty(me_id))
            {
                HasNew = chat.Messages.Where(m => (m.UserSenderId != me_id && m.IsNew)).Any();
                if (chat.UserFromId == me_id)
                {
                    Name = chat.UserTo.Name;
                    ChatIconPath = chat.UserTo.File.Path;
                    IsBlock = chat.UserTo.BlackListsFrom.Where(m => m.UserToId == me_id).Any();
                }
                else
                {
                    Name = chat.UserFrom.Name;
                    ChatIconPath = chat.UserFrom.File.Path;
                    IsBlock = chat.UserFrom.BlackListsFrom.Where(m => m.UserToId == me_id).Any();
                }
            }
            var last = chat.Messages.LastOrDefault();
            LastSenderAvatarPath = last.UserSender.File.Path;
            LastMessage = (last.Text.Length > 15) ? last.Text.Substring(0, 15) + " ..."
                                                  : last.Text; 
            LastMessage = last.Text;
            LastMessageDateTime = last.DateSend;
            HasNew = chat.Messages.Where(m => m.IsNew == true).Any();
        }
    }
}
