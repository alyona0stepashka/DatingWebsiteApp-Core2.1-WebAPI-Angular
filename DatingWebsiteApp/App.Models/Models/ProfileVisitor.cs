using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models 
{
    public class ProfileVisitor : EntityBase
    { 
        public string OwnerProfileId { get; set; }
        public string VisitorId { get; set; }
        public DateTime LastVisit { get; set; }

        public virtual ApplicationUser OwnerProfile { get; set; } 
        public virtual ApplicationUser Visitor { get; set; }

    }
}
