using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class BlackList
    {
        public int Id { get; set; }

        public string User1Id { get; set; }

        public string User2Id { get; set; }

        public virtual ApplicationUser User1 { get; set; }

        public virtual ApplicationUser User2 { get; set; }
    }
}
