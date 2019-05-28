using App.Models;
using System;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ApplicationUser, string> Users { get; set; }
        IRepository<BlackList,int> BlackLists { get; set; }
        IRepository<Chat, int> Chats { get; set; }
        IRepository<ChatMessage, int> ChatMessages { get; set; }
        IRepository<FileModel, int> FileModels { get; set; }
        IRepository<Friendship, int> Friendships { get; set; }
        IRepository<PersonalType, int> PersonalTypes { get; set; }
        IRepository<PhotoAlbum, int> PhotoAlbums { get; set; }
        IStaticRepository<BadHabit, int> BadHabits { get; set; }
        IRepository<BadHabitUser, int> BadHabitUsers { get; set; }
        IStaticRepository<Education, int> Educations { get; set; }
        IStaticRepository<FamilyStatus, int> FamilyStatuses { get; set; }
        IStaticRepository<FinanceStatus, int> FinanceStatuses { get; set; }
        IStaticRepository<Interest, int> Interests { get; set; }
        IRepository<InterestUser, int> InterestUsers { get; set; }
        IStaticRepository<Language, int> Languages { get; set; }
        IRepository<LanguageUser, int> LanguageUsers { get; set; }
        IStaticRepository<Nationality, int> Nationalities { get; set; }
        IStaticRepository<Sex, int> Sexes { get; set; }
        IStaticRepository<Zodiac, int> Zodiacs { get; set; } 
        Task SaveAsync();
    }
}
