using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateSend { get; set; }

        public string UserId { get; set; }

        public int ChatId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Chat Chat { get; set; }

        public virtual List<FileModel> Files { get; set; }
    }
}
