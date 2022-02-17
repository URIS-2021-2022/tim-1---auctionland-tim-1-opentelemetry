using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Etities
{
    public class AddressContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AddressContext(DbContextOptions<AddressContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasData(new
                {
                    AddressID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Street = "Devet Jugovica",
                    Number = 89,
                    ZipCode = "21203",
                    CityID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    CityName = "Veternik",
                    CountryID = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    CountryName = "Srbija"
                });

            modelBuilder.Entity<Address>()
               .HasData(new
               {
                   AddressID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   Street = "Svetozara Miletica",
                   Number = 2,
                   ZipCode = "21203",
                   CityID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                   CityName = "Veternik",
                   CountryID = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                   CountryName = "Srbija"
               });
        }
    }
}
