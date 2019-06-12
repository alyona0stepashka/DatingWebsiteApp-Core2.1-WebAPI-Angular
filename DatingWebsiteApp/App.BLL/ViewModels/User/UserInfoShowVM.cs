using App.BLL.ViewModels;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.BLL.ViewModels
{
    public class UserInfoShowVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAnonimus { get; set; }

        public bool? IsFriend { get; set; }

        public bool? IsBlack { get; set; }

        public string PhotoPath { get; set; }

        public DateTime DateBirth { get; set; }

        public StaticBaseVM Sex { get; set; }

        public StaticBaseVM MainGoal { get; set; }

        public StaticBaseVM FamilyStatus { get; set; }

        public StaticBaseVM FinanceStatus { get; set; }

        public StaticBaseVM Education { get; set; }

        public StaticBaseVM Nationality { get; set; }

        public StaticBaseVM Zodiac { get; set; }

        public List<StaticBaseVM> Languages { get; set; }

        public List<StaticBaseVM> BadHabits { get; set; }

        public List<StaticBaseVM> Interests { get; set; }

        public double Growth { get; set; }

        public double Weight { get; set; }

        public UserInfoShowVM()
        {

        }

        public UserInfoShowVM(ApplicationUser user, string my_id)
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
                Sex = new StaticBaseVM {Id=user.SexId.Value, Value = user.Sex.Value };
            }
            if (user.MainGoal != null)
            {
                MainGoal = new StaticBaseVM { Id = user.MainGoalId.Value, Value = user.MainGoal.Value };
            }
            if (user.Type != null)
            {
                if (user.Type.FamilyStatus != null)
                {
                    FamilyStatus = new StaticBaseVM { Id = user.Type.FamilyStatusId.Value, Value = user.Type.FamilyStatus.Value };
                }
                if (user.Type.FinanceStatus != null)
                {
                    FinanceStatus = new StaticBaseVM { Id = user.Type.FinanceStatusId.Value, Value = user.Type.FinanceStatus.Value };
                }
                if (user.Type.Education != null)
                {
                    Education = new StaticBaseVM { Id = user.Type.EducationId.Value, Value = user.Type.Education.Value };
                }
                if (user.Type.Nationality != null)
                {
                    Nationality = new StaticBaseVM { Id = user.Type.NationalityId.Value, Value = user.Type.Nationality.Value };
                }
                if (user.Type.Zodiac != null)
                {
                    Zodiac = new StaticBaseVM { Id = user.Type.ZodiacId.Value, Value = user.Type.Zodiac.Value };
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
                    Languages = new List<StaticBaseVM>();
                    var db_langs = user.Type.Languages;
                    foreach (var lang in db_langs)
                    {
                        Languages.Add(new StaticBaseVM { Id = lang.Language.Id, Value = lang.Language.Value });
                    }
                }
                if (user.Type.BadHabits != null)
                {
                    BadHabits = new List<StaticBaseVM>();
                    var db_habitss = user.Type.BadHabits;
                    foreach (var habit in db_habitss)
                    {
                        BadHabits.Add(new StaticBaseVM { Id = habit.BadHabit.Id, Value = habit.BadHabit.Value });
                    }
                }
                if (user.Type.Interests != null)
                {
                    Interests = new List<StaticBaseVM>();
                    var db_interests = user.Type.Interests;
                    foreach (var inter in db_interests)
                    {
                        Interests.Add(new StaticBaseVM { Id = inter.Interest.Id, Value = inter.Interest.Value });
                    }
                }
                IsBlack = false;
                IsFriend = false;
                if (my_id != null)
                {
                    if (user.BlackListsFrom != null)
                    {
                        if (user.BlackListsFrom.Where(m => m.UserToId == my_id).Any())
                        {
                            IsBlack = true;
                        } 
                    } 

                    if (user.FriendshipsFrom != null)
                    {
                        if (user.FriendshipsFrom.Where(m => m.UserToId == my_id/* && m.Status==true*/).Any())
                        {
                            IsFriend = true;
                        } 
                    } 
                    if (user.FriendshipsTo != null)
                    {
                        if (user.FriendshipsTo.Where(m => m.UserFromId == my_id/* && m.Status == true*/).Any())
                        {
                            IsFriend = true;
                        } 
                    } 
                } 
            }
        }
    }
}
