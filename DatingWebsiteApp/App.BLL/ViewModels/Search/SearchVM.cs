using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class SearchVM
    {
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public List<int> BadHabit { get; set; }
        public List<int> Education { get; set; }
        public List<int> FamilyStatus { get; set; }
        public List<int> FinanceStatus { get; set; }
        public List<int> Interest { get; set; }
        public List<int> Language { get; set; }
        public List<int> MainGoal { get; set; }
        public List<int> Nationality { get; set; }
        public List<int> Sex { get; set; }
        public List<int> Zodiac { get; set; } 

        public bool? NetworkStatus { get; set; } 

        public SearchVM()
        {

        }
    }
}
