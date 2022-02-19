using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Entities;

namespace UserMicroservice.Entities
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration configuration;

        public UserContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>()
                .HasData(new UserType
                {
                    UserTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    UserTypeName = "Operater",
                });
            modelBuilder.Entity<UserType>()
               .HasData(new UserType
               {
                   UserTypeId = Guid.Parse("1b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   UserTypeName = "Tehnički sekretar",
               });
            modelBuilder.Entity<UserType>()
                .HasData(new UserType
                {
                    UserTypeId = Guid.Parse("9b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    UserTypeName = "Prva komisija",
                });
            modelBuilder.Entity<UserType>()
                .HasData(new UserType
                {
                    UserTypeId = Guid.Parse("3c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    UserTypeName = "Superuser",
                });
            modelBuilder.Entity<UserType>()
                .HasData(new UserType
                {
                    UserTypeId = Guid.Parse("9a7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    UserTypeName = "Operater Nadmetanja",
                });
            modelBuilder.Entity<UserType>()
                .HasData(new UserType
                {
                    UserTypeId = Guid.Parse("7b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    UserTypeName = "Licitant",
                });
            modelBuilder.Entity<UserType>()
               .HasData(new UserType
               {
                   UserTypeId = Guid.Parse("5c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   UserTypeName = "Menadžer",
               });
           modelBuilder.Entity<UserType>()
              .HasData(new UserType
              {
                  UserTypeId = Guid.Parse("3a7ea607-8ddb-493a-87fa-4bf5893e965b"),
                  UserTypeName = "Administrator",
              });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    FirstName = "Petar",
                    LastName = "Petrović",
                    Username = "Petar123",
                    Email = "petrović@gmail.com",
                    Password = "12345",
                    UserTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                });
        }
    }
}
