using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserInfoShowVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAnonimus { get; set; }

        public string PhotoPath { get; set; }

        public DateTime DateBirth { get; set; }

        public string Sex { get; set; }

        public string MainGoal { get; set; }

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

        public UserInfoShowVM()
        {

        }

        public UserInfoShowVM(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            IsAnonimus = user.IsAnonimus;
            if (user.File != null)
            {
                PhotoPath = user.File.Path;
            }
            DateBirth = user.DateBirth;
            if (user.Sex != null)
            {
                Sex = user.Sex.Value;
            }
            MainGoal = user.MainGoal;
            if (user.Type != null)
            {
                if (user.Type.FamilyStatus != null)
                {
                    FamilyStatus = user.Type.FamilyStatus.Value;
                }
                if (user.Type.FinanceStatus != null)
                {
                    FinanceStatus = user.Type.FinanceStatus.Value;
                }
                if (user.Type.Education != null)
                {
                    Education = user.Type.Education.Value;
                }
                if (user.Type.Nationality != null)
                {
                    Nationality = user.Type.Nationality.Value;
                }
                if (user.Type.Zodiac != null)
                {
                    Zodiac = user.Type.Zodiac.Value;
                }
                if (user.Type.Growth != null)
                {
                    Growth = user.Type.Growth.Value;
                }
                if (user.Type.Weight != null)
                {
                    Weight = user.Type.Weight.Value;
                }
                if (user.Type.Languages != null)
                {
                    Languages = new List<string>();
                    var db_langs = user.Type.Languages;
                    foreach (var lang in db_langs)
                    {
                        Languages.Add(lang.Language.Value);
                    }
                }
                if (user.Type.BadHabits != null)
                {
                    BadHabits = new List<string>();
                    var db_habitss = user.Type.BadHabits;
                    foreach (var lang in db_habitss)
                    {
                        BadHabits.Add(lang.BadHabit.Value);
                    }
                }
                if (user.Type.Interests != null)
                {
                    Interests = new List<string>();
                    var db_interests = user.Type.Interests;
                    foreach (var lang in db_interests)
                    {
                        Interests.Add(lang.Interest.Value);
                    }
                }
            }
        }
    }
}
