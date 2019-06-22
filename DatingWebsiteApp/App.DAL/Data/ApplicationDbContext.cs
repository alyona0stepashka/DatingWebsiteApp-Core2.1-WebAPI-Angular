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
        public DbSet<ProfileVisitor> ProfileVisitors { get; set; }
        public DbSet<ChatRoom> Chats { get; set; }
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
        public DbSet<MainGoal> MainGoals { get; set; }
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
            modelBuilder.Entity<Friendship>()
                .HasOne(a => a.UserTo)
                .WithMany(b => b.FriendshipsTo)
                .HasForeignKey(c => c.UserToId);

            modelBuilder.Entity<Friendship>()
                .HasOne(a => a.UserFrom)
                .WithMany(b => b.FriendshipsFrom)
                .HasForeignKey(c => c.UserFromId);

            modelBuilder.Entity<ProfileVisitor>()
                .HasOne(m => m.OwnerProfile)
                .WithMany(l => l.ProfileOwner)
                .HasForeignKey(p => p.OwnerProfileId);

            modelBuilder.Entity<ProfileVisitor>()
                .HasOne(m => m.Visitor)
                .WithMany(l => l.ProfileVisitor)
                .HasForeignKey(p => p.VisitorId);

            modelBuilder.Entity<ChatRoom>()
                .HasOne(a => a.UserFrom)
                .WithMany(b => b.ChatsFrom)
                .HasForeignKey(c => c.UserFromId);

            modelBuilder.Entity<ChatRoom>()
                .HasOne(a => a.UserTo)
                .WithMany(b => b.ChatsTo)
                .HasForeignKey(c => c.UserToId);

            modelBuilder.Entity<BlackList>()
                .HasOne(a => a.UserFrom)
                .WithMany(b => b.BlackListsFrom)
                .HasForeignKey(c => c.UserFromId);

            modelBuilder.Entity<BlackList>()
                .HasOne(a => a.UserTo)
                .WithMany(b => b.BlackListsTo)
                .HasForeignKey(c => c.UserToId);

            modelBuilder.Entity<FileModel>()
                .HasOne(m => m.PhotoAlbum)
                .WithMany(m => m.Files)
                .HasForeignKey(m => m.PhotoAlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.UserSender)
                .WithMany(m => m.ChatMessages)
                .HasForeignKey(m => m.UserSenderId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Chat)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<FileModel>()
                .HasOne(m => m.PhotoAlbum)
                .WithMany(m => m.Files)
                .HasForeignKey(m => m.PhotoAlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FileModel>()
                .HasOne(m => m.Message)
                .WithMany(m => m.Files)
                .HasForeignKey(m => m.MessageId); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
