﻿using App.DAL.Interfaces;
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
            var zodiac_id = 0;
            if (!db.Zodiacs.GetAll().Any())
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
                    zodiac_id = z.Id;
                }
            }
            //-------------------
            var sex_id = 0;
            if (!db.Sexes.GetAll().Any())
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
                    sex_id = z.Id;
                }

            }
            //-------------------
            var nat_id = 0;
            if (!db.Nationalities.GetAll().Any())
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
                    nat_id = z.Id;
                }
            }
            //------------------- 
            if (!db.Languages.GetAll().Any())
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
            if (!db.Interests.GetAll().Any())
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
            var fin_id = 0;
            if (!db.FinanceStatuses.GetAll().Any())
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
                    fin_id = z.Id;
                }
            }
            //-------------------
            var fam_id = 0;
            if (!db.FamilyStatuses.GetAll().Any())
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
                    fam_id = z.Id;
                }
            }
            //-------------------
            var educ_id = 0;
            if (!db.Educations.GetAll().Any())
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
                    educ_id = z.Id;
                }
            }
            //-------------------
            if (!db.BadHabits.GetAll().Any())
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
            var photo_no_image = (db.FileModels.GetWhere(m => m.Name == "no-image.jpg")).FirstOrDefault();  //ищем фото-заглушку
            if (photo_no_image == null)  //если ее нет в бд - создаем
            {
                photo_no_image = await db.FileModels.CreateAsync(new FileModel
                {
                    Name = "no-image.jpg",
                    Path = "/Images/App/no-image.jpg"
                });
            } 
            //---------------------
            if (!userManager.Users.Any())
            {
                var new_user = new ApplicationUser
                {
                    Email="user@mail.ru",
                    UserName = "user@mail.ru",
                    Name = "TestUser",
                    EmailConfirmed = true,
                    FileId = photo_no_image.Id,
                    SexId = sex_id,
                    Type = new PersonalType
                    {
                        Growth=1.67,
                        Weight=54,
                        EducationId = educ_id,
                        FamilyStatusId = fam_id,
                        FinanceStatusId = fin_id,
                        NationalityId = nat_id,
                        ZodiacId = zodiac_id
                    }
                };
                await userManager.CreateAsync(new_user, "Parol_01");
            }
        }
    }
}

