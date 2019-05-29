using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class Chat : EntityBase
    {
        //public int Id { get; set; }

        public bool Status { get; set; }

        [ForeignKey("UserFrom")]
        public string UserFromId { get; set; }

        [ForeignKey("UserTo")]
        public string UserToId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public virtual ApplicationUser UserTo { get; set; }
    }
}
