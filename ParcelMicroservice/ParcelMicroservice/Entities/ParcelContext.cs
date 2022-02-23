using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Entities
{
    public class ParcelContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ParcelContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<PartOfParcel> PartOfParcels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParcelDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartOfParcel>().HasKey(table => new {
                table.ParcelID,
                table.PartOfParcelID
            });

            modelBuilder.Entity<PartOfParcel>().HasOne(v => v.Parcel).WithMany().HasForeignKey(v => v.ParcelID);

            modelBuilder.Entity<Parcel>()
                .HasData(new
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    SurfaceArea = 6000,
                    NumberOfParcel = "1/2022",
                    RealEstateListNumber = "1 - Prepis",
                    CultureRealStatus = "Njive",
                    ClassRealStatus = "III",
                    WorkabilityActualStatus = "Obradivo",
                    ProtectedZoneActualStatus = "2",
                    FormOfOwnership = "Državna svojina",
                    DrainageActualCondition = "Površinsko odvodnjavanje",
                    MunicipalityID = Guid.Parse("e4902805-41f9-45d7-8b4d-7c85e8f27868"),
                    NameOfTheMunicipality = "Čantavir"
                });

            modelBuilder.Entity<Parcel>()
                .HasData(new
                {
                    ParcelID = Guid.Parse("628f7390-cb85-4b69-94bd-e3e6c424d725"),
                    SurfaceArea = 3000,
                    NumberOfParcel = "2/2022",
                    RealEstateListNumber = "2 - Prepis",
                    CultureRealStatus = "Pašnjaci",
                    ClassRealStatus = "II",
                    WorkabilityActualStatus = "Ostalo",
                    ProtectedZoneActualStatus = "1",
                    FormOfOwnership = "Društvena svojina",
                    DrainageActualCondition = "Podzemno odvodnjavanje",
                    MunicipalityID = Guid.Parse("aa3f2265-7182-4424-ba83-2eed388ce748"),
                    NameOfTheMunicipality = "Bajmok"
                });

            modelBuilder.Entity<PartOfParcel>()
                .HasData(new
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    PartOfParcelID = Guid.Parse("67c31d49-b189-4de5-a6e2-b9b5557047a9"),
                    SurfaceAreaPOP = 1000,
                    ClassID = Guid.Parse("61847780-396a-42a7-8e04-941e0d4eddf9"),
                    ClassLandLabel = "I"
                });

            modelBuilder.Entity<PartOfParcel>()
                .HasData(new
                {
                    ParcelID = Guid.Parse("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                    PartOfParcelID = Guid.Parse("17321524-7822-4daa-8134-a2ec4bed98e0"),
                    SurfaceAreaPOP = 500,
                    ClassID = Guid.Parse("89e2bdc2-7153-463a-8c9f-37bfec240431"),
                    ClassLandLabel = "II"
                });
        }
    }
}
