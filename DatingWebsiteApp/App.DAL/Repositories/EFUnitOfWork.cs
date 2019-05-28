using App.DAL.Data;
using App.DAL.Interfaces;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace App.DAL.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private Repository<ApplicationUser, string> _user;
        private Repository<BlackList, int> _blackLists;
        private Repository<Chat, int> _chats;
        private Repository<ChatMessage, int> _chatMessages;
        private Repository<FileModel, int> _fileModels;
        private Repository<Friendship, int> _friendships;
        private Repository<PersonalType, int> _personalTypes;
        private Repository<PhotoAlbum, int> _photoAlbums;
        private StaticRepository<BadHabit, int> _badHabits;
        private Repository<BadHabitUser, int> _badHabitUsers;
        private StaticRepository<Education, int> _educations;
        private StaticRepository<FamilyStatus, int> _familyStatuses;
        private StaticRepository<FinanceStatus, int> _financeStatuses;
        private StaticRepository<Interest, int> _interests;
        private Repository<InterestUser, int> _interestUsers;
        private StaticRepository<Language, int> _languages;
        private Repository<LanguageUser, int> _languageUsers;
        private StaticRepository<Nationality, int> _nationalities;
        private StaticRepository<Sex, int> _sexes;
        private StaticRepository<Zodiac, int> _zodiacs; 

        public EfUnitOfWork(DbContextOptions options)
        {
            _db = new ApplicationDbContext(options);
        }

        public IRepository<ApplicationUser, string> Users => _user ?? (_user = new Repository<ApplicationUser,string>(_db));
        public IRepository<BlackList, int> BlackLists => _blackLists ?? (_blackLists = new Repository<BlackList, int>(_db));
        public IRepository<Chat, int> Chats => _chats ?? (_chats = new Repository<Chat, int>(_db));
        public IRepository<ChatMessage, int> ChatMessages => _chatMessages ?? (_chatMessages = new Repository<ChatMessage, int>(_db));
        public IRepository<FileModel, int> FileModels => _fileModels ?? (_fileModels = new Repository<FileModel, int>(_db));
        public IRepository<Friendship, int> Friendships => _friendships ?? (_friendships = new Repository<Friendship, int>(_db));
        public IRepository<PersonalType, int> PersonalTypes => _personalTypes ?? (_personalTypes = new Repository<PersonalType, int>(_db));
        public IRepository<PhotoAlbum, int> PhotoAlbums => _photoAlbums ?? (_photoAlbums = new Repository<PhotoAlbum, int>(_db));
        public IStaticRepository<BadHabit, int> BadHabits => _badHabits ?? (_badHabits = new StaticRepository<BadHabit, int>(_db));
        public IRepository<BadHabitUser, int> BadHabitUsers => _badHabitUsers ?? (_badHabitUsers = new Repository<BadHabitUser, int>(_db));
        public IStaticRepository<Education, int> Educations => _educations ?? (_educations = new StaticRepository<Education, int>(_db));
        public IStaticRepository<FamilyStatus, int> FamilyStatuses => _familyStatuses ?? (_familyStatuses = new StaticRepository<FamilyStatus, int>(_db));
        public IStaticRepository<FinanceStatus, int> FinanceStatuses => _financeStatuses ?? (_financeStatuses = new StaticRepository<FinanceStatus, int>(_db));
        public IStaticRepository<Interest, int> Interests => _interests ?? (_interests = new StaticRepository<Interest, int>(_db));
        public IRepository<InterestUser, int> InterestUsers => _interestUsers ?? (_interestUsers = new Repository<InterestUser, int>(_db));
        public IStaticRepository<Language, int> Languages => _languages ?? (_languages = new StaticRepository<Language, int>(_db));
        public IRepository<LanguageUser, int> LanguageUsers => _languageUsers ?? (_languageUsers = new Repository<LanguageUser, int>(_db));
        public IStaticRepository<Nationality, int> Nationalities => _nationalities ?? (_nationalities = new StaticRepository<Nationality, int>(_db));
        public IStaticRepository<Sex, int> Sexes => _sexes ?? (_sexes = new StaticRepository<Sex, int>(_db));
        public IStaticRepository<Zodiac, int> Zodiacs => _zodiacs ?? (_zodiacs = new StaticRepository<Zodiac, int>(_db)); 

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;
            if (disposing)
            {
                _db.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
