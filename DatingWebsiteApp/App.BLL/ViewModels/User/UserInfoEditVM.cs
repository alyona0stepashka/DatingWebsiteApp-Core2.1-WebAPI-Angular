using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserInfoEditVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBirth { get; set; }

        public bool? IsAnonimus { get; set; }

        public int? Sex { get; set; }

        public int? MainGoal { get; set; }

        public int? FamilyStatus { get; set; }

        public int? FinanceStatus { get; set; }

        public int? Education { get; set; }

        public int? Nationality { get; set; }

        public int? Zodiac { get; set; }

        public double? Growth { get; set; }

        public double? Weight { get; set; }

        public List<string> Languages { get; set; }

        public List<string> BadHabits { get; set; }

        public List<string> Interest { get; set; }

        public UserInfoEditVM()
        {

        }
    }
}
