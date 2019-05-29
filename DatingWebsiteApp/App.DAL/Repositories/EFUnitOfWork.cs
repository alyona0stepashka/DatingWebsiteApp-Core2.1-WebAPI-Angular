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
        private Repository<BlackList> _blackLists;
        private Repository<Chat> _chats;
        private Repository<ChatMessage> _chatMessages;
        private Repository<FileModel> _fileModels;
        private Repository<Friendship> _friendships;
        private Repository<PersonalType> _personalTypes;
        private Repository<PhotoAlbum> _photoAlbums;
        private StaticRepository<BadHabit> _badHabits;
        private Repository<BadHabitUser> _badHabitUsers;
        private StaticRepository<Education> _educations;
        private StaticRepository<FamilyStatus> _familyStatuses;
        private StaticRepository<FinanceStatus> _financeStatuses;
        private StaticRepository<Interest> _interests;
        private Repository<InterestUser> _interestUsers;
        private StaticRepository<Language> _languages;
        private Repository<LanguageUser> _languageUsers;
        private StaticRepository<Nationality> _nationalities;
        private StaticRepository<Sex> _sexes;
        private StaticRepository<Zodiac> _zodiacs; 

        public EfUnitOfWork(DbContextOptions options)
        {
            _db = new ApplicationDbContext(options);
        }
         
        public IRepository<BlackList> BlackLists => _blackLists ?? (_blackLists = new Repository<BlackList>(_db));
        public IRepository<Chat> Chats => _chats ?? (_chats = new Repository<Chat>(_db));
        public IRepository<ChatMessage> ChatMessages => _chatMessages ?? (_chatMessages = new Repository<ChatMessage>(_db));
        public IRepository<FileModel> FileModels => _fileModels ?? (_fileModels = new Repository<FileModel>(_db));
        public IRepository<Friendship> Friendships => _friendships ?? (_friendships = new Repository<Friendship>(_db));
        public IRepository<PersonalType> PersonalTypes => _personalTypes ?? (_personalTypes = new Repository<PersonalType>(_db));
        public IRepository<PhotoAlbum> PhotoAlbums => _photoAlbums ?? (_photoAlbums = new Repository<PhotoAlbum>(_db));
        public IStaticRepository<BadHabit> BadHabits => _badHabits ?? (_badHabits = new StaticRepository<BadHabit>(_db));
        public IRepository<BadHabitUser> BadHabitUsers => _badHabitUsers ?? (_badHabitUsers = new Repository<BadHabitUser>(_db));
        public IStaticRepository<Education> Educations => _educations ?? (_educations = new StaticRepository<Education>(_db));
        public IStaticRepository<FamilyStatus> FamilyStatuses => _familyStatuses ?? (_familyStatuses = new StaticRepository<FamilyStatus>(_db));
        public IStaticRepository<FinanceStatus> FinanceStatuses => _financeStatuses ?? (_financeStatuses = new StaticRepository<FinanceStatus>(_db));
        public IStaticRepository<Interest> Interests => _interests ?? (_interests = new StaticRepository<Interest>(_db));
        public IRepository<InterestUser> InterestUsers => _interestUsers ?? (_interestUsers = new Repository<InterestUser>(_db));
        public IStaticRepository<Language> Languages => _languages ?? (_languages = new StaticRepository<Language>(_db));
        public IRepository<LanguageUser> LanguageUsers => _languageUsers ?? (_languageUsers = new Repository<LanguageUser>(_db));
        public IStaticRepository<Nationality> Nationalities => _nationalities ?? (_nationalities = new StaticRepository<Nationality>(_db));
        public IStaticRepository<Sex> Sexes => _sexes ?? (_sexes = new StaticRepository<Sex>(_db));
        public IStaticRepository<Zodiac> Zodiacs => _zodiacs ?? (_zodiacs = new StaticRepository<Zodiac>(_db));

        

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
