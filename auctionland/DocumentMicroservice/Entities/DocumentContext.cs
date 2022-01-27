using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class DocumentContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DocumentContext(DbContextOptions<DocumentContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }
        public DbSet<ListOfDocuments> ListOfDocuments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DocumentDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListOfDocuments>().HasData(new
            {
                ListOfDocumentsId = Guid.Parse("9791d7f8-ee0e-4426-9bdb-2600d8aa8975"),
                ListCreationDate = DateTime.Parse("2010-11-24T10:00:00")
            });

            modelBuilder.Entity<ListOfDocuments>().HasData(new
            {
                ListOfDocumentsId = Guid.Parse("7297a3c3-5674-4537-9ce2-1319de481a27"),
                ListCreationDate = DateTime.Parse("2008-12-20T10:00:00")
            });

            modelBuilder.Entity<Document>().HasData(new
            {
                DocumentId = Guid.Parse("8b44e760-3c55-4ae4-8d1b-c4ea053672ff"),
                DocumentSerialNumber = 1,
                DocumentName = "First",
                DocumentDate = DateTime.Parse("2008-12-20T10:00:00"),
                DocumentSubmissionDate = DateTime.Parse("2021-12-20T10:00:00"),
                DocumentTemplate = "FTemplate",
                ListOfDocumentsID = Guid.Parse("9791d7f8-ee0e-4426-9bdb-2600d8aa8975")
            });

            modelBuilder.Entity<Document>().HasData(new
            {
                DocumentId = Guid.Parse("300f7843-4343-4520-ac1c-995d8095ea89"),
                DocumentSerialNumber = 2,
                DocumentName = "Second",
                DocumentDate = DateTime.Parse("2020-12-20T10:00:00"),
                DocumentSubmissionDate = DateTime.Parse("2008-12-20T10:00:00"),
                DocumentTemplate = "STemplate",
                ListOfDocumentsID = Guid.Parse("7297a3c3-5674-4537-9ce2-1319de481a27")
            });

            modelBuilder.Entity<LeaseAgreement>()
                .HasData(new
                {
                    LeaseAgreementID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    LeaseTypeOfGuarantee = "FLease",
                    LeaseAgreementMaturities = DateTime.Parse("2020-11-10T10:00:00"),
                    LeaseAgreementEntryDate = DateTime.Parse("2020-11-15T09:00:00"),
                    MinisterID = Guid.Parse("02daa4a7-7602-44e1-a0f0-f2bfeeceef5e"),
                    PublicBiddingID = Guid.Parse("f29575a1-37ce-4a94-998f-13e3119232fd"),
                    PersonID = Guid.Parse("20d61140-9027-494f-a544-0112c3ee0f57"),
                    DeadlineForLandRestitution = DateTime.Parse("2020-11-18T10:00:00"),
                    PlaceOfSigning = "Some place",
                    DateOfSigning = DateTime.Parse("2020-11-20T10:00:00")
                });

            modelBuilder.Entity<LeaseAgreement>()
                .HasData(new
                {
                    LeaseAgreementID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    LeaseTypeOfGuarantee = "SLease",
                    LeaseAgreementMaturities = DateTime.Parse("2020-11-10T10:00:00"),
                    LeaseAgreementEntryDate = DateTime.Parse("2020-11-13T09:00:00"),
                    MinisterID = Guid.Parse("02daa4a7-7602-44e1-a0f0-f2bfeeceef5e"),
                    PublicBiddingID = Guid.Parse("f29575a1-37ce-4a94-998f-13e3119232fd"),
                    PersonID = Guid.Parse("a4927ae1-52e0-4b8e-bcfc-9b353cb3a7e1"),
                    DeadlineForLandRestitution = DateTime.Parse("2020-11-23T10:00:00"),
                    PlaceOfSigning = "Second place",
                    DateOfSigning = DateTime.Parse("2020-11-28T10:00:00")
                });
        }
    }
}
