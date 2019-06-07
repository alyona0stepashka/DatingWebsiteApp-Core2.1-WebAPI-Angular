using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class SearchVM
    {
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? BadHabit { get; set; }
        public int? Education { get; set; }
        public int? FamilyStatus { get; set; }
        public int? FinanceStatus { get; set; }
        public int? Interest { get; set; }
        public int? Language { get; set; }
        public int? MainGoal { get; set; }
        public int? Nationality { get; set; }
        public int? Sex { get; set; }
        public int? Zodiac { get; set; } 

        public bool? NetworkStatus { get; set; } 

        public SearchVM()
        {

        }
    }
}
