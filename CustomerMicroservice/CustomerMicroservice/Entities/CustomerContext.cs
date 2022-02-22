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

        public CustomerContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        //public DbSet<Customer> Customer { get; set; }
        public DbSet<LegallyPerson> LegallyPerson { get; set; }
        public DbSet<PhysicalPerson> PhysicalPerson { get; set; }
        public DbSet<AuthorizedPerson> AuthorizedPerson { get; set; }
        public DbSet<AuthorizedPersonCustomer> AuthorizedPersonCustomer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CustomerDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhysicalPerson>().HasData(
                new
                {
                    CustomerID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3"),
                    JMBG = "1106985770036",
                    FirstName = "Marko",
                    LastName = "Markovic",
                    PhoneNumber1 = "0652365574",
                    PhoneNumber2 = "0632456685",
                    Email = "marko@gmail.com",
                    AccountNumber = "36105666854456",
                    IsPhysicalPerson = true,
                    Priority = "Nizak",
                    RealizedArea = "10ha",
                    HasABan = true,
                    StartDateBan = DateTime.Parse("2018-10-13T09:00:00"),
                    DurationBan = 3,
                    EndDateBan = DateTime.Parse("2021-10-13T09:00:00"),
                    AddressId = Guid.Parse("1b411c11-a196-48f7-88bd-67596c3974c1"),
                    DocumentID = Guid.Parse("3a411c13-a196-48f7-88bd-57596c3974b3"),
                    PublicBiddingID = Guid.Parse("3b411c13-a186-48f7-78bd-57596c3974b3")
                });

            modelBuilder.Entity<LegallyPerson>().HasData(
                new
                {
                    CustomerID = Guid.Parse("4a411c13-a196-48f7-88bd-67596c3974c4"),
                    IdentificationNumber = "325687964",
                    Name = "Context",
                    Fax = "021352664",
                    PhoneNumber1 = "066451235",
                    PhoneNumber2 = "0643214458",
                    Email = "context@info.com",
                    AccountNumber = "3204568885231",
                    IsPhysicalPerson = false,
                    Priority = "Visok",
                    RealizedArea = "38ha",
                    HasABan = true,
                    StartDateBan = DateTime.Parse("2020-11-13T09:00:00"),
                    DurationBan = 1,
                    EndDateBan = DateTime.Parse("2021-11-13T09:00:00"),
                    AddressId = Guid.Parse("5b411c15-a196-48f7-88bd-67596c3974c5"),
                    DocumentID = Guid.Parse("6a411c13-6196-48f7-88bd-57596c3974b6"),
                    PublicBiddingID = Guid.Parse("5a411c13-a146-48f7-78bd-57896c3974b3")
                });

            modelBuilder.Entity<AuthorizedPerson>().HasData(
                new AuthorizedPerson
                {
                    AuthorizedPersonID = Guid.Parse("4a151b19-a287-47f7-61bd-4a596c397461"),
                    FirstName = "Nikola",
                    LastName = "Nikolic",
                    NumberPersonalDocument = "012345788",
                    BoardTable = "1",
                    AddressId = Guid.Parse("2a151b12-a287-47f2-61bd-4a596c397461"),
                    PublicBiddingID = Guid.Parse("6b411c13-a186-48f7-78ad-57896c3944b1")
                },
                new AuthorizedPerson
                {
                    AuthorizedPersonID = Guid.Parse("4b151b29-a287-47f7-61bd-2a596c397431"),
                    FirstName = "Nemanja",
                    LastName = "Nenadovic",
                    NumberPersonalDocument = "025445739",
                    BoardTable = "2",
                    AddressId = Guid.Parse("3a151b32-a287-47f3-61bd-4a596c397463"),
                    PublicBiddingID = Guid.Parse("2b411c13-a186-28f7-78bd-57296c3974b0")
                });

            modelBuilder.Entity<AuthorizedPersonCustomer>().HasData(
                new AuthorizedPersonCustomer
                {
                    AuthorizedPersonCustomerID = Guid.Parse("5b151b29-a285-47f7-61bd-2a596c397425"),
                    AuthorizedPersonID = Guid.Parse("4a151b19-a287-47f7-61bd-4a596c397461"),
                    CustomerID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3")
                });
        }
    }
}
