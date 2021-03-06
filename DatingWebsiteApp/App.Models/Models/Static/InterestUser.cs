﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models 
{
    public class InterestUser : EntityBase
    {
       // public int Id { get; set; }

        //[ForeignKey("PersonalType")]
        public int PersonalTypeId { get; set; }

       // [ForeignKey("Interest")]
        public int InterestId { get; set; }

        public virtual PersonalType PersonalType { get; set; }

        public virtual Interest Interest { get; set; }

    }
}
