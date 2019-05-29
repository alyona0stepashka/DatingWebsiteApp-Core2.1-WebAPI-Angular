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

        public string Sex { get; set; }

        public string  MainGoal { get; set; }

        public string FamilyStatus { get; set; }

        public string FinanceStatus { get; set; }

        public string Education { get; set; }

        public string Nationality { get; set; }

        public string Zodiac { get; set; }

        public List<string> Languages { get; set; }

        public List<string> BadHabits { get; set; }

        public List<string> Interest { get; set; }

        public double? Growth { get; set; }

        public double? Weight { get; set; }

        public UserInfoEditVM()
        {

        }
    }
}
