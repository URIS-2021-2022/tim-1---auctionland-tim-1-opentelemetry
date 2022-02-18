using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Kreirana klasa koja implementira DbContext
    /// </summary>
    public class AdContext : DbContext
    {

        /// <summary>
        /// Kreiran konstruktor
        /// </summary>
        /// <param name="options"></param>
        public AdContext(DbContextOptions<AdContext> options) : base(options)
        {
       
        }

        /// <summary>
        /// Metoda koja služi za čuvanje i pokretanje upita instanci klase AD
        /// </summary>
        public DbSet<Ad> Ads { get; set; }

        /// <summary>
        /// Kreiranje i inicijalno punjenje modela 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ad>()
                .HasData(new
                {
                    AdID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    AdName = "Prodaja placa kod pijace",
                    OfficialListID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    MunicipalityName = "Čantavir",
                    OfficialListNumber = "1/2022",
                    DateOfIssue = DateTime.Parse("2020-11-15T09:00:00")
                });

            builder.Entity<Ad>()
                .HasData(new
                {
                    AdID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    AdName = "Prodaja placa pored crkve",
                    OfficialListID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    MunicipalityName = "Žednik",
                    OfficialListNumber = "2/2022",
                    DateOfIssue = DateTime.Parse("2020-11-16T09:00:00")
                });
        }
    }
}
