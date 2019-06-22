using App.Models;
using System;
using System.Threading.Tasks;

namespace App.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 
        IRepository<BlackList> BlackLists { get; }
        IStaticRepository<ProfileVisitor> ProfileVisitors { get; }
        IRepository<ChatRoom> Chats { get; }
        IRepository<ChatMessage> ChatMessages { get;}
        IRepository<FileModel> FileModels { get; }
        IRepository<Friendship> Friendships { get; }
        IRepository<PersonalType> PersonalTypes { get; }
        IRepository<PhotoAlbum> PhotoAlbums { get; }
        IStaticRepository<BadHabit> BadHabits { get; }
        IRepository<BadHabitUser> BadHabitUsers { get; }
        IStaticRepository<Education> Educations { get; }
        IStaticRepository<FamilyStatus> FamilyStatuses { get;}
        IStaticRepository<FinanceStatus> FinanceStatuses { get;  }
        IStaticRepository<Interest> Interests { get; }
        IRepository<InterestUser> InterestUsers { get;  }
        IStaticRepository<Language> Languages { get; }
        IRepository<LanguageUser> LanguageUsers { get; }
        IStaticRepository<Nationality> Nationalities { get; }
        IStaticRepository<Sex> Sexes { get; }
        IStaticRepository<MainGoal> MainGoals { get; }
        IStaticRepository<Zodiac> Zodiacs { get; } 
        Task SaveAsync();
    }
}
