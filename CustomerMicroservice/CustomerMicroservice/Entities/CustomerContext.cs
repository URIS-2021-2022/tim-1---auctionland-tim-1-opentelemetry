using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class CustomerContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CustomerContext(DbContextOptions<CustomerContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<LegallyPerson> LegallyPeople { get; set; }
        public DbSet<PhysicalPerson> PhysicalPeople { get; set; }
        public DbSet<AuthorizedPerson> AuthorizedPeople { get; set; }
        public DbSet<AuthorizedPersonCustomer> AuthorizedPersonCustomers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CustomerDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(b =>
            {
                
            });

            modelBuilder.Entity<PhysicalPerson>(b =>
            {

            });

            modelBuilder.Entity<LegallyPerson>(b =>
            {

            });

            modelBuilder.Entity<AuthorizedPerson>(b =>
            {

            });

            modelBuilder.Entity<AuthorizedPersonCustomer>(b =>
            {

            });
        }
    }
}
