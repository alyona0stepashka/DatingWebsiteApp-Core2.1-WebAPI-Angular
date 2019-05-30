using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        //[ForeignKey("Sex")]
        public int? SexId { get; set; }  

        public string MainGoal { get; set; }  

        public bool IsAnonimus { get; set; }

        public int ProfileViewsForMonth { get; set; }

        public int IncomingFirstMessageCount { get; set; }

        public int OutgoingFirstMessageCount { get; set; }

        //[ForeignKey("Type")]
        public int? TypeId { get; set; }

        //[ForeignKey("File")]
        public int? FileId { get; set; }

        public virtual Sex Sex { get; set; } 

        public virtual PersonalType Type { get; set; }

        public virtual FileModel File { get; set; }

        public virtual List<Friendship> FriendshipsFrom { get; set; }

        public virtual List<Friendship> FriendshipsTo { get; set; }

        public virtual List<ChatMessage> ChatMessages { get; set; }

        public virtual List<Chat> ChatsFrom { get; set; }

        public virtual List<Chat> ChatsTo { get; set; }

        public virtual List<BlackList> BlackListsFrom { get; set; }

        public virtual List<BlackList> BlackListsTo { get; set; }

        //public ApplicationUser()
        //{
        //    Sex = new Sex();
        //    Type = new PersonalType();
        //    File = new FileModel();
        //    FriendshipsTo = new List<Friendship>();
        //    FriendshipsFrom = new List<Friendship>();
        //    ChatMessages = new List<ChatMessage>();
        //    ChatsFrom = new List<Chat>();
        //    ChatsTo = new List<Chat>();
        //    BlackListsFrom = new List<BlackList>();
        //    BlackListsTo = new List<BlackList>();
        //} 
    }
}
