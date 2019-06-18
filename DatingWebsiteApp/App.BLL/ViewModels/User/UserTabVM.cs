using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserTabVM
    {
        public string Id { get; set; }

        public string Name { get; set; }  

        public string PhotoPath { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public string MainGoal { get; set; }

        public UserTabVM(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name; 
            if (user.File != null)
            {
                PhotoPath = user.File.Path;
            }
            Age = DateTime.Now.Year - user.DateBirth.Year;
            if (user.Sex != null)
            {
                Sex = user.Sex.Value;
            }
            if (user.MainGoal != null)
            {
                MainGoal = user.MainGoal.Value;
            } 
        }
        public UserTabVM()
        {

        }
    }
}
