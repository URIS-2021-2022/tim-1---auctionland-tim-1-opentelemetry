﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintContext : DbContext
    {
        public ComplaintContext(DbContextOptions<ComplaintContext> options) : base(options) //dodaje neke dodatne opcije koje su definisane okviru startup klase
        {

        }

        public DbSet<Complaint> Complaints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Complaint>()
                .HasData(new
                {
                    ComplaintId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintStatus = "Rejected",
                    ComplaintStatusId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    Action = "Public bidding does not go to the second round",
                    DateOfDecision = DateTime.Parse("2020-11-15T09:00:00"),
                    Explanation = "Explanation",
                    NumberOfDecision = "1",
                    Reason = "Reason",
                    SubmissionDate = DateTime.Parse("2020-11-13T09:00:00"),
                    ComplaintTypeName = "Appeal against the course of the public tender",
                    ComplaintTypeId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f")
                });

            builder.Entity<Complaint>()
                .HasData(new
                {
                    ComplaintId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ComplaintStatus = "Rejected",
                    ComplaintStatusId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    Action = "Public bidding does not go to the second round",
                    DateOfDecision = DateTime.Parse("2020-11-15T09:00:00"),
                    Explanation = "Explanation",
                    NumberOfDecision = "2",
                    Reason = "Reason",
                    SubmissionDate = DateTime.Parse("2020-11-13T09:00:00"),
                    ComplaintTypeName = "Appeal against the Decision on Leasing",
                    ComplaintTypeId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3")
                });
        }
    }
}
