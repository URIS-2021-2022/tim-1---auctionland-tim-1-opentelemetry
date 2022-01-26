using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class LeaseAgreementContext : DbContext
    {
        private readonly IConfiguration configuration;

        public LeaseAgreementContext(DbContextOptions<LeaseAgreementContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DocumentDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasData(new
                {
                    LeaseAgreementID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    LeaseTypeOfGuarantee = "Some type",
                    LeaseAgreementMaturities = DateTime.Parse("2020-11-10T10:00:00"),
                    LeaseAgreementEntryDate = DateTime.Parse("2020-11-15T09:00:00"),
                    DeadlineForLandRestitution = DateTime.Parse("2020-11-18T10:00:00"),
                    PlaceOfSigning = "Some place",
                    DateOfSigning = DateTime.Parse("2020-11-20T10:00:00")
                });

            modelBuilder.Entity<Document>()
                .HasData(new
                {
                    LeaseAgreementID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    LeaseTypeOfGuarantee = "Second type",
                    LeaseAgreementMaturities = DateTime.Parse("2020-11-10T10:00:00"),
                    LeaseAgreementEntryDate = DateTime.Parse("2020-11-13T09:00:00"),
                    DeadlineForLandRestitution = DateTime.Parse("2020-11-23T10:00:00"),
                    PlaceOfSigning = "Second place",
                    DateOfSigning = DateTime.Parse("2020-11-28T10:00:00")
                });
        }
    }
}
