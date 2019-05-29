using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class ChatMessage : EntityBase
    {
        //public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateSend { get; set; }

        //[ForeignKey("UserSender")]
        public string UserSenderId { get; set; }

       // [ForeignKey("Chat")]
        public int ChatId { get; set; }

        public bool IsReaded { get; set; }

        public virtual ApplicationUser UserSender { get; set; }

        public virtual Chat Chat { get; set; }

        public virtual List<FileModel> Files { get; set; }
    }
}
