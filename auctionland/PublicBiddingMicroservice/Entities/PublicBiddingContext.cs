using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class PublicBiddingContext : DbContext
    {
        private readonly IConfiguration configuration;

        public PublicBiddingContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<PublicBidding> PublicBiddings { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PublicBiddingDB"));
        }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim etapama
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PublicBidding>()
                .HasData(new
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
                    Status = "Status1",
                });

            builder.Entity<Stage>()
                .HasData(new
                {
                    StageId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Date = DateTime.Parse("2020-11-15T09:00:00"),
                    
                });
            builder.Entity<Auction>()
               .HasData(new
               {
                   AuctionId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                   Number = 1,
                   Year = 2021,
                   Date = DateTime.Parse("2020-11-15T09:00:00"),
                   PriceIncrease = 10.00,
                   ApplicationDeadline = DateTime.Parse("2020-11-15T09:00:00")

               });
        }
    }
}
