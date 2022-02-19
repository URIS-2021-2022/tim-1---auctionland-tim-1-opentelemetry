using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace PublicBiddingMicroservice.Entities
{
    public class PublicBiddingContext : DbContext
    {
        private readonly IConfiguration configuration;

        public PublicBiddingContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<PublicBidding> PublicBidding { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Auction> Auction { get; set; }
        public DbSet<Status> Status { get;  set; }
        public DbSet<PublicBiddingType> PublicBiddingType { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PublicBiddingDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim etapama
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage>()
                .HasData(new Stage
                {
                    StageId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Date = DateTime.Parse("2020-11-15T09:00:00"),
                });

            modelBuilder.Entity<Status>()
                .HasData(new Status
                {
                    StatusId = Guid.Parse("1f7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StatusName = "Prvi krug",
                });
            modelBuilder.Entity<Status>()
               .HasData(new Status
               {
                   StatusId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   StatusName = "Drugi krug sa starim uslovima",
               });

            modelBuilder.Entity<Status>()
               .HasData(new Status
               {
                   StatusId = Guid.Parse("1a7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   StatusName = "Drugi krug sa novim uslovima",
               });


            modelBuilder.Entity<PublicBiddingType>()
                .HasData(new PublicBiddingType
                {
                    PublicBiddingTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PublicBiddingTypeName = "Javna lictacija",
                });

            modelBuilder.Entity<PublicBiddingType>()
               .HasData(new PublicBiddingType
               {
                   PublicBiddingTypeId = Guid.Parse("1f7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   PublicBiddingTypeName = "Otvaranje zatvorenih ponuda",
               });

            modelBuilder.Entity<PublicBidding>()
                .HasData(new PublicBidding
                {
                    PublicBiddingId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Date = DateTime.Parse("2020-11-15T09:00:00"),
                    StartTime = DateTime.Parse("2020-11-15T09:00:00"),
                    EndTime = DateTime.Parse("2020-11-15T12:00:00"),
                    StartingPricePerHe = 10.00,
                    Exclude = true,
                    AuctionedPrice = 139.00,
                    LeasePeriod = 1,
                    NumberOfParticipants = 12,
                    DepositReplenishment = 13.00,
                    Circle = 1,
                    StatusId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StageId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PublicBiddingTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                });

            modelBuilder.Entity<Auction>()
               .HasData(new Auction
               {
                   AuctionId = Guid.Parse("4c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   Number = 1,
                   Year = 2021,
                   Date = DateTime.Parse("2020-11-15T09:00:00"),
                   PriceIncrease = 10.00,
                   ApplicationDeadline = DateTime.Parse("2020-11-15T09:00:00"),
                   StageId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
               });
        }
    }
}
