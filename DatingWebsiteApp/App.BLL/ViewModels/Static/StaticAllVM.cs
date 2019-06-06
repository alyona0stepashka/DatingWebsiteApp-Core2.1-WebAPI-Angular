using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class StaticAllVM
    {
        public List<BadHabit> BadHabits { get; set; }
        public List<Education> Educations { get; set; }
        public List<FamilyStatus> FamilyStatuses { get; set; }
        public List<FinanceStatus> FinanceStatuses { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Language> Languages { get; set; }
        public List<MainGoal> MainGoals { get; set; }
        public List<Nationality> Nationalities { get; set; }
        public List<Sex> Sexes { get; set; }
        public List<Zodiac> Zodiacs { get; set; }

        public StaticAllVM()
        {

        }
    }
}
