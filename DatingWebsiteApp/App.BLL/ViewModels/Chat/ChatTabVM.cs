using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using System.Linq;
using App.BLL.Chat;

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

        public DateTime? IsOnline { get; set; }

        public ChatTabVM(ChatRoom chat, string me_id)
        {
            Id = chat.Id;
            IsBlock = false;
            HasNew = false;
            if (!String.IsNullOrEmpty(me_id))
            {
                //chat.Messages = chat.UserFromId == me_id ? chat.Messages.Where(m => m.DateSend > chat.UserFromClearHistory).ToList()
                                                         //: chat.Messages.Where(m => m.DateSend > chat.UserToClearHistory).ToList();

                HasNew = chat.Messages.Where(m => (m.UserSenderId != me_id && m.IsNew)).Any();
                var user_he = (chat.UserFromId == me_id) ? chat.UserTo
                                                      : chat.UserFrom;
                //var user_me = (chat.UserToId == me_id) ? chat.UserTo
                //                                      : chat.UserFrom;
                Name = user_he.Name;
                ChatIconPath = user_he.File.Path;
                IsBlock = user_he.BlackListsFrom.Where(m => m.UserToId == me_id).Any();
                IsOnline = ChatHub.connects.Any(m => m.UserId == user_he.Id) ? (DateTime?)null
                                                                          : user_he.DateLastOnline;
            }
            var last = chat.Messages.LastOrDefault();
            LastSenderAvatarPath = last.UserSender.File.Path;

            LastMessage = (last.Text.Length > 15) ? last.Text.Substring(0, 15) + " ..."
                                                  : last.Text;  

            LastMessageDateTime = last.DateSend; 
        }
    }
}
