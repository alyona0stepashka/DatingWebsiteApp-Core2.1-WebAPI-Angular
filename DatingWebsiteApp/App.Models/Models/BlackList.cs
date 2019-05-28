using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class BlackList
    {
        public int Id { get; set; }

        public string UserFromId { get; set; }

        public string UserToId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public virtual ApplicationUser UserTo { get; set; }
    }
}
