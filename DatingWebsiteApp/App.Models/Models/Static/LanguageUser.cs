using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class LanguageUser : EntityBase
    {
        //public int Id { get; set; }

        //[ForeignKey("PersonalType")]
        public string PersonalTypeId { get; set; }

        //[ForeignKey("Language")]
        public int LanguageId { get; set; }

        public virtual PersonalType PersonalType { get; set; }

        public virtual Language Language { get; set; }
    }
}
