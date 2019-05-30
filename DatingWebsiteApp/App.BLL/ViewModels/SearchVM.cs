using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class SearchVM
    {
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string  Sex { get; set; }
        public string FamilyStatus { get; set; }
        public string MainGoal { get; set; }

        //public int MyProperty { get; set; }  ???Type

        public bool? NetworkStatus { get; set; } 

        public SearchVM()
        {

        }
    }
}
