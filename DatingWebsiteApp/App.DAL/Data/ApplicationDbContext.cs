using App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<PersonalType> PersonalTypes { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<BadHabit> BadHabits { get; set; }
        public DbSet<BadHabitUser> BadHabitUsers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<FamilyStatus> FamilyStatuses { get; set; }
        public DbSet<FinanceStatus> FinanceStatuses { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<InterestUser> InterestUsers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageUser> LanguageUsers { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Zodiac> Zodiacs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendship>().HasOne(m=>m.)



            modelBuilder.Entity<Friendship>()
                .HasOne(a => a.UserFrom)
                .WithMany(b => b.Friendships)
                .HasForeignKey(c => c.UserFromId);

            modelBuilder.Entity<UsersRelationship>()
                .HasRequired(a => a.RequestedTo)
                .WithMany(b => b.ReceievedFriendRequests)
                .HasForeignKey(c => c.RequestedToId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
