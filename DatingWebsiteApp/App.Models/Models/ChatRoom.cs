using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class ChatRoom : EntityBase
    {
        //public int Id { get; set; } 

        //[ForeignKey("UserFrom")]
        public string UserFromId { get; set; }

        //[ForeignKey("UserTo")]
        public string UserToId { get; set; }

        public bool IsBlock { get; set; }

        public DateTime UserFromClearHistory { get; set; }

        public DateTime UserToClearHistory { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public virtual ApplicationUser UserTo { get; set; }

        public virtual List<ChatMessage> Messages { get; set; }

        //public Chat()
        //{
        //    UserFrom = new ApplicationUser();
        //    UserTo = new ApplicationUser();
        //}
    }
}
