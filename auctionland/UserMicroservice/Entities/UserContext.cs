using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace UserMicroservice.Entities
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration configuration;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
        {
           this.configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));
        }

    }
}
