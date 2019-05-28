using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Sex
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public virtual ApplicationUser Users { get; set; }

    }
}
