using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        //[ForeignKey("Sex")]
        public int? SexId { get; set; }  

        public int? MainGoalId { get; set; }  

        public bool IsAnonimus { get; set; }

        public int ProfileViewsForMonth { get; set; }

        public int IncomingFirstMessageCount { get; set; }

        public int OutgoingFirstMessageCount { get; set; }

        //[ForeignKey("Type")]
        public int? TypeId { get; set; }

        //[ForeignKey("File")]
        public int? FileId { get; set; }

        public virtual FileModel File { get; set; }

        public virtual Sex Sex { get; set; }

        public virtual PersonalType Type { get; set; }

        public virtual MainGoal MainGoal { get; set; }

        public virtual IQueryable<Friendship> FriendshipsFrom { get; set; }

        public virtual IQueryable<Friendship> FriendshipsTo { get; set; } 

        public virtual IQueryable<ChatMessage> ChatMessages { get; set; }

        public virtual IQueryable<ChatRoom> ChatsFrom { get; set; }

        public virtual IQueryable<ChatRoom> ChatsTo { get; set; }

        public virtual IQueryable<BlackList> BlackListsFrom { get; set; }

        public virtual IQueryable<BlackList> BlackListsTo { get; set; }

        public virtual IQueryable<PhotoAlbum> PhotoAlbums { get; set; }

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
