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
            modelBuilder.Entity<Customer>().HasData(
                new
                {
                    CustomerID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3"),
                    Priority = "Nizak",
                    RealizedArea = "10ha",
                    HasABan = true,
                    StartDateBan = DateTime.Parse("2018-10-13T09:00:00"),
                    DurationBan = 3,
                    EndDateBan = DateTime.Parse("2021-10-13T09:00:00"),
                    //AddressID = Guid.Parse("1b411c11-a196-48f7-88bd-67596c3974c1"),
                    //DocumentsID = Guid.Parse("3a411c13-a196-48f7-88bd-57596c3974b3")
                },
                new 
                {
                    CustomerID = Guid.Parse("4a411c13-a196-48f7-88bd-67596c3974c4"),
                    Priority = "Visok",
                    RealizedArea = "38ha",
                    HasABan = true,
                    StartDateBan = DateTime.Parse("2020-11-13T09:00:00"),
                    DurationBan = 1,
                    EndDateBan = DateTime.Parse("2021-11-13T09:00:00"),
                    //AddressID = Guid.Parse("5b411c15-a196-48f7-88bd-67596c3974c5"),
                    //DocumentsID = Guid.Parse("6a411c13-6196-48f7-88bd-57596c3974b6")
                });

            modelBuilder.Entity<PhysicalPerson>().HasData(
                new
                {
                    CustomerID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3"),
                    PhysicalPersonID = Guid.Parse("7a411c13-a197-48f7-88bd-68596c3974c4"),
                    JMBG = "1106985770036",
                    FirstName = "Marko",
                    LastName = "Markovic",
                    PhoneNumber1 = "0652365574",
                    PhoneNumber2 = "0632456685",
                    Email = "marko@gmail.com",
                    AccountNumber = "36105666854456"
                });

            modelBuilder.Entity<LegallyPerson>().HasData(
                new
                {
                    CustomerID = Guid.Parse("4a411c13-a196-48f7-88bd-67596c3974c4"),
                    LegallyPersonID = Guid.Parse("4a411b19-a197-48f7-88bd-6a596c397467"),
                    IdentificationNumber = "325687964",
                    Name = "Context",
                    Fax = "021352664",
                    PhoneNumber1 = "066451235",
                    PhoneNumber2 = "0643214458",
                    Email = "context@info.com",
                    AccountNumber = "3204568885231"
                });

            modelBuilder.Entity<AuthorizedPerson>().HasData(
                new
                {
                    AuthorizedPersonID = Guid.Parse("4a151b19-a287-47f7-61bd-4a596c397461"),
                    FirstName = "Nikola",
                    LastName = "Nikolic",
                    NumberPersonalDocument = "012345788",
                    BoardTable = "1",
                    //AddressID = Guid.Parse("2a151b12-a287-47f2-61bd-4a596c397461")
                },
                new
                {
                    AuthorizedPersonID = Guid.Parse("4b151b29-a287-47f7-61bd-2a596c397431"),
                    FirstName = "Nemanja",
                    LastName = "Nenadovic",
                    NumberPersonalDocument = "025445739",
                    BoardTable = "2",
                    //AddressID = Guid.Parse("3a151b32-a287-47f3-61bd-4a596c397463")
                });

            modelBuilder.Entity<AuthorizedPersonCustomer>().HasData(
                new
                {
                    AuthorizedPersonCustomerID = Guid.Parse("5b151b29-a285-47f7-61bd-2a596c397425"),
                    AuthorizedPersonID = Guid.Parse("4a151b19-a287-47f7-61bd-4a596c397461"),
                    CustomerID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3")
                });
        }
    }
}
