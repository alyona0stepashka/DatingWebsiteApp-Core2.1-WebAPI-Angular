using App.DAL.Interfaces;
using App.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task InitializeAsync(IUnitOfWork db, UserManager<ApplicationUser> userManager)
        {
            //-------------------
            if (!db.Zodiacs.GetAllAsync().Any())
            {
                var list = new List<Zodiac>()
                {
                    new Zodiac{Value="Aries"},
                    new Zodiac{Value="Taurus"},
                    new Zodiac{Value="Gemini"},
                    new Zodiac{Value="Cancer"},
                    new Zodiac{Value="Leo"},
                    new Zodiac{Value="Virgo"},
                    new Zodiac{Value="Libra"},
                    new Zodiac{Value="Scorpio"},
                    new Zodiac{Value="Sagittarius"},
                    new Zodiac{Value="Capricorn"},
                    new Zodiac{Value="Acquarius"},
                    new Zodiac{Value="Pisces"}
                };
                foreach (var z in list)
                {
                    await db.Zodiacs.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.Sexes.GetAllAsync().Any())
            {
                var list = new List<Sex>()
                {
                    new Sex{Value="Male"},
                    new Sex{Value="Female"},
                    //new Sex{Value="Bi"}
                };
                foreach (var z in list)
                {
                    await db.Sexes.CreateAsync(z);
                }

            }
            //-------------------
            if (!db.Nationalities.GetAllAsync().Any())
            {
                var list = new List<Nationality>()
                {
                    new Nationality{Value="Chinese"},
                    new Nationality{Value="Arab"},
                    new Nationality{Value="American"}, 
                    new Nationality{Value="Brazilian"},
                    new Nationality{Value="Russian"},
                    new Nationality{Value="European"}
                };
                foreach (var z in list)
                {
                    await db.Nationalities.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.Languages.GetAllAsync().Any())
            {
                var list = new List<Language>()
                {
                    new Language{Value="Chinese"},
                    new Language{Value="Arab"},
                    new Language{Value="English"},
                    new Language{Value="Brazilian"},
                    new Language{Value="Russian"},
                    new Language{Value="Spanish"}
                };
                foreach (var z in list)
                {
                    await db.Languages.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.Interests.GetAllAsync().Any())
            {
                var list = new List<Interest>()
                {
                    new Interest{Value="IT"},
                    new Interest{Value="Swimming"},
                    new Interest{Value="Gaming"},
                    new Interest{Value="Dancing"},
                    new Interest{Value="Watching films"},
                    new Interest{Value="Cooking"}
                };
                foreach (var z in list)
                {
                    await db.Interests.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.FinanceStatuses.GetAllAsync().Any())
            {
                var list = new List<FinanceStatus>()
                {
                    new FinanceStatus{Value="Poor"},
                    new FinanceStatus{Value="Standart"},
                    new FinanceStatus{Value="Rich"} 
                };
                foreach (var z in list)
                {
                    await db.FinanceStatuses.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.FamilyStatuses.GetAllAsync().Any())
            {
                var list = new List<FamilyStatus>()
                {
                    new FamilyStatus{Value="In active search"},
                    new FamilyStatus{Value="Divorsed"},
                    new FamilyStatus{Value="Widow"},
                    new FamilyStatus{Value="In relationship"},
                    new FamilyStatus{Value="Married"}
                };
                foreach (var z in list)
                {
                    await db.FamilyStatuses.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.Educations.GetAllAsync().Any())
            {
                var list = new List<Education>()
                {
                    new Education{Value="None"},
                    new Education{Value="Secondary"},
                    new Education{Value="Higher"}
                };
                foreach (var z in list)
                {
                    await db.Educations.CreateAsync(z);
                }
            }
            //-------------------
            if (!db.BadHabits.GetAllAsync().Any())
            {
                var list = new List<BadHabit>()
                {
                    new BadHabit{Value="Smoking"},
                    new BadHabit{Value="Drinking"},
                    new BadHabit{Value="Drugs"}
                };
                foreach (var z in list)
                {
                    await db.BadHabits.CreateAsync(z);
                }
            }
            //---------------------
            if (!userManager.Users.Any())
            {
                var new_user = new ApplicationUser
                {
                    Email="user@mail.ru",
                    UserName = "user@mail.ru",
                    Name = "TestUser",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(new_user, "Parol_01");
            }
        }
    }
}

