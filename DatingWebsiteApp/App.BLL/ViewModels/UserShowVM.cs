using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserShowVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAnonimus { get; set; }

        public string PhotoPath { get; set; }

        public DateTime DateBirth { get; set; }

        public string Sex { get; set; }

        public string  MainGoal { get; set; }

        public string FamilyStatus { get; set; }

        public string FinanceStatus { get; set; }

        public string Education { get; set; }

        public string Nationality { get; set; }

        public string Zodiac { get; set; }

        public List<string> Languages { get; set; }

        public List<string> BadHabits { get; set; }

        public List<string> Interests { get; set; }

        public double Growth { get; set; }

        public double Weight { get; set; }

        public UserShowVM(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            IsAnonimus = user.IsAnonimus;
            PhotoPath = user.File.Path;
            DateBirth = user.DateBirth;
            Sex = user.Sex.Value;
            MainGoal = user.MainGoal;
            FamilyStatus = user.Type.FamilyStatus.Value;
            FinanceStatus = user.Type.FinanceStatus.Value;
            Education = user.Type.Education.Value;
            Nationality = user.Type.Nationality.Value;
            Zodiac = user.Type.Zodiac.Value;
            Growth = user.Type.Growth.Value;
            Weight = user.Type.Weight.Value;
            Languages = new List<string>();
            var db_langs = user.Type.Languages;
            foreach (var lang in db_langs)
            {
                Languages.Add(lang.Language.Value);
            }
            BadHabits = new List<string>();
            var db_habitss = user.Type.BadHabits;
            foreach (var lang in db_habitss)
            {
                BadHabits.Add(lang.BadHabit.Value);
            }
            Interests = new List<string>();
            var db_interests = user.Type.Interests;
            foreach (var lang in db_interests)
            {
                Interests.Add(lang.Interest.Value);
            }
        }
        public UserShowVM()
        {

        }
    }
}
