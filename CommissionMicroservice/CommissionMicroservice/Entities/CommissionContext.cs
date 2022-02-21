using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Entities
{
    public class CommissionContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CommissionContext(DbContextOptions<CommissionContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Commission> Commissions { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CommissionDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commission>(b =>
            {
                b.HasData(new
                {
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NameCommission = "Prva komisija"
                },
                new
                {
                    CommissionID = Guid.Parse("8a211c13-a195-48f7-8dbd-67596c3974c0"),
                    NameCommission = "Druga komisija"
                });

                /*b.OwnsOne(e => e.President).HasData(new
                {
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    MemberID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3"),
                    FirstName = "Marko",
                    LastName = "Markovic",
                    Role = "Predsednik"
                });

                b.OwnsMany(e => e.MembersOfCommission).HasData(new
                {
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    MemberID = Guid.Parse("3a411c13-a196-48f7-88bd-67596c3974c2"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Role = "Clan"
                });

                b.OwnsMany(e => e.MembersOfCommission).HasData(new
                {
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    MemberID = Guid.Parse("4a411c13-a196-48f7-88bd-67596c3974c4"),
                    FirstName = "Jovan",
                    LastName = "Jovanovic",
                    Role = "Clan"
                });*/
            });

            modelBuilder.Entity<Member>().HasData(
                new
                { 
                    MemberID = Guid.Parse("2a411c13-a196-48f7-88bd-67596c3974c3"),
                    FirstName = "Marko",
                    LastName = "Markovic",
                    Role = "Predsednik",
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NameCommission = "Prva komisija"
                },
                new
                {
                    MemberID = Guid.Parse("3a411c13-a196-48f7-88bd-67596c3974c2"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Role = "Clan",
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NameCommission = "Prva komisija"
                },
                new
                {
                    MemberID = Guid.Parse("4a411c13-a196-48f7-88bd-67596c3974c4"),
                    FirstName = "Jovan",
                    LastName = "Jovanovic",
                    Role = "Clan",
                    CommissionID = Guid.Parse("8a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NameCommission = "Prva komisija"
                });
        }
    }
}
