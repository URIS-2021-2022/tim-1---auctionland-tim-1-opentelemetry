﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMicroservice.Entities;

namespace UserMicroservice.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20220201204613_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserMicroservice.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            Email = "petrović@gmail.com",
                            FirstName = "Petar",
                            LastName = "Petrović",
                            Password = "12345",
                            UserTypeId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            Username = "Petar123"
                        });
                });

            modelBuilder.Entity("UserMicroservice.Entities.UserType", b =>
                {
                    b.Property<Guid>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserType");

                    b.HasData(
                        new
                        {
                            UserTypeId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Operater"
                        },
                        new
                        {
                            UserTypeId = new Guid("1b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Tehnički sekretar"
                        },
                        new
                        {
                            UserTypeId = new Guid("9b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Prva komisija"
                        },
                        new
                        {
                            UserTypeId = new Guid("3c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Superuser"
                        },
                        new
                        {
                            UserTypeId = new Guid("9a7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Operater Nadmetanja"
                        },
                        new
                        {
                            UserTypeId = new Guid("7b7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Licitant"
                        },
                        new
                        {
                            UserTypeId = new Guid("5c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Menadžer"
                        },
                        new
                        {
                            UserTypeId = new Guid("3a7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UserTypeName = "Administrator"
                        });
                });

            modelBuilder.Entity("UserMicroservice.Entities.User", b =>
                {
                    b.HasOne("UserMicroservice.Entities.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId");

                    b.Navigation("UserType");
                });
#pragma warning restore 612, 618
        }
    }
}
