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

        public UserContext(DbContextOptions options, IConfiguration configuration) //: base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*builder.Entity<User>()
                .HasData(new
                {
                    Id = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    FirstName = "Petar",
                    LastName = "Petrović",
                    Username = "Petar123",
                    Email = "petrović@gmail.com",
                    Password = "12345",
                    UserTypeId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    UserTypeName = "Type1",
                });

            builder.Entity<User>()
                .HasData(new
                {
                    Id = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    FirstName = "Marko",
                    LastName = "Marković",
                    Username = "Marko123",
                    Email = "marković@gmail.com",
                    Password = "444",
                    UserTypeId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    UserTypeName = "Type1",
                });*/
        }
    }
}
