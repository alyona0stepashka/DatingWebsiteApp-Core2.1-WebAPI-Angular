using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Friendship
    {
        public int Id { get; set; }

        public bool Status { get; set; }

        public string User1Id { get; set; }

        public string User2Id { get; set; }

        public virtual ApplicationUser User1 { get; set; }

        public virtual ApplicationUser User2 { get; set; }
    }
}
