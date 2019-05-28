using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Models
{
    public class PersonalType
    {
        public int Id { get; set; }

        [ForeignKey("FamilyStatus")]
        public int FamilyStatusId { get; set; }  

        public double Growth { get; set; }

        public double Weight { get; set; }

        [ForeignKey("Education")]
        public int EducationId { get; set; }

        [ForeignKey("Nationality")]
        public int NationalityId { get; set; }

        [ForeignKey("Zodiac")]
        public int ZodiacId { get; set; }

        [ForeignKey("FinanceStatus")]
        public int FinanceStatusId { get; set; }  

        public virtual ApplicationUser User { get; set; }

        public virtual FinanceStatus FinanceStatus { get; set; }

        public virtual FamilyStatus FamilyStatus { get; set; }

        public virtual Education Education { get; set; }

        public virtual Nationality Nationality { get; set; }

        public virtual Zodiac Zodiac { get; set; }

        public virtual List<LanguageUser> Languages { get; set; }

        public virtual List<BadHabitUser> BadHabits { get; set; }

        public virtual List<InterestUser> Interests { get; set; }
    }
}
