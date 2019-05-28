using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public string UserFromId { get; set; }

        public string UserToId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public virtual ApplicationUser UserTo { get; set; }
    }
}
