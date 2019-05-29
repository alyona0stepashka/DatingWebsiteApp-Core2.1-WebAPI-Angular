using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models 
{
    public class BadHabitUser : EntityBase
    {
        //public int Id { get; set; }

        //[ForeignKey("PersonalType")]
        public string PersonalTypeId { get; set; }

        //[ForeignKey("BadHabit")]
        public int BadHabitId { get; set; }

        public virtual PersonalType PersonalType { get; set; }

        public virtual BadHabit BadHabit { get; set; }

    }
}
