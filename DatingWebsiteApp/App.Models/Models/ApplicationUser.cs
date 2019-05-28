using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public string Sex { get; set; }  //???

        public string MainGoal { get; set; }  //???

        public string Interests { get; set; } //??? like string or table fk

        public bool IsAnonimus { get; set; }

        public int ProfileViewsForMonth { get; set; }

        public double ReplyRate { get; set; }

        public int TypeId { get; set; }

        public int FileId { get; set; }

        public virtual PersonalType Type { get; set; }

        public virtual FileModel File { get; set; }

        public List<Friendship> Friendships { get; set; }

        public List<ChatMessage> ChatMessages { get; set; }

        public List<Chat> Chats { get; set; }
    }
}
