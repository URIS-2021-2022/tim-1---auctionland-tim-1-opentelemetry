using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationContext(DbContextOptions options, IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<ApplicationForPublicBidding> ApplicationForPublicBidding { get; set; }
        public DbSet<PaymentForApplication> paymentForApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationForPublicBiddinfDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationForPublicBidding>().HasData(new
            {
                ApplicationId = Guid.Parse("a4927ae1-52e0-4b8e-bcfc-9b353cb3a7e1"),
                PaymentId = Guid.Parse("cf72ddaf-66ff-435c-8129-940b25ad943d"),
                BuyerId = Guid.Parse("ab91c3fc-e095-4c38-b2a6-4117de3ac0b9")
            });

            builder.Entity<PaymentForApplication>().HasData(new
            {
                PaymentId = Guid.Parse("8ac08292-9406-45cf-aff4-0c9447880779"),
                AccountNumber = 154564,
                ReferenceNumber = 8554654684,
                PurposeOfPayment = "Some purpose",
                DateOfPayment = DateTime.Parse("2020-11-15T09:00:00"),
                PublicBiddingId = Guid.Parse("7344fd85-ab0e-4da3-9fa4-d65af38e001f")
            });
        }
    }
}
