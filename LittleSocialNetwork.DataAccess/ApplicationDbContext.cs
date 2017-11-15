﻿
using LittleSocialNetwork.DataAccess.Configurations;
using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleSocialNetwork.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SingleChatMessageConfiguration());
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}