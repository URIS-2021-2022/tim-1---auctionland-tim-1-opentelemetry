﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParcelMicroservice.Entities;

namespace ParcelMicroservice.Migrations
{
    [DbContext(typeof(ParcelContext))]
    partial class ParcelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParcelMicroservice.Entities.Parcel", b =>
                {
                    b.Property<Guid>("ParcelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassRealStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CultureRealStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrainageActualCondition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormOfOwnership")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MunicipalityID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameOfTheMunicipality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfParcel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProtectedZoneActualStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealEstateListNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurfaceArea")
                        .HasColumnType("int");

                    b.Property<string>("WorkabilityActualStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParcelID");

                    b.ToTable("Parcels");

                    b.HasData(
                        new
                        {
                            ParcelID = new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                            ClassRealStatus = "III",
                            CultureRealStatus = "Njive",
                            DrainageActualCondition = "Površinsko odvodnjavanje",
                            FormOfOwnership = "Državna svojina",
                            MunicipalityID = new Guid("e4902805-41f9-45d7-8b4d-7c85e8f27868"),
                            NameOfTheMunicipality = "Čantavir",
                            NumberOfParcel = "1/2022",
                            ProtectedZoneActualStatus = "2",
                            RealEstateListNumber = "1 - Prepis",
                            SurfaceArea = 6000,
                            WorkabilityActualStatus = "Obradivo"
                        },
                        new
                        {
                            ParcelID = new Guid("628f7390-cb85-4b69-94bd-e3e6c424d725"),
                            ClassRealStatus = "II",
                            CultureRealStatus = "Pašnjaci",
                            DrainageActualCondition = "Podzemno odvodnjavanje",
                            FormOfOwnership = "Društvena svojina",
                            MunicipalityID = new Guid("aa3f2265-7182-4424-ba83-2eed388ce748"),
                            NameOfTheMunicipality = "Bajmok",
                            NumberOfParcel = "2/2022",
                            ProtectedZoneActualStatus = "1",
                            RealEstateListNumber = "2 - Prepis",
                            SurfaceArea = 3000,
                            WorkabilityActualStatus = "Ostalo"
                        });
                });

            modelBuilder.Entity("ParcelMicroservice.Entities.PartOfParcel", b =>
                {
                    b.Property<Guid>("ParcelID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PartOfParcelID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassLandLabel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurfaceAreaPOP")
                        .HasColumnType("int");

                    b.HasKey("ParcelID", "PartOfParcelID");

                    b.ToTable("PartOfParcels");

                    b.HasData(
                        new
                        {
                            ParcelID = new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                            PartOfParcelID = new Guid("67c31d49-b189-4de5-a6e2-b9b5557047a9"),
                            ClassID = new Guid("61847780-396a-42a7-8e04-941e0d4eddf9"),
                            ClassLandLabel = "I",
                            SurfaceAreaPOP = 1000
                        },
                        new
                        {
                            ParcelID = new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"),
                            PartOfParcelID = new Guid("17321524-7822-4daa-8134-a2ec4bed98e0"),
                            ClassID = new Guid("89e2bdc2-7153-463a-8c9f-37bfec240431"),
                            ClassLandLabel = "II",
                            SurfaceAreaPOP = 500
                        });
                });

            modelBuilder.Entity("ParcelMicroservice.Entities.PartOfParcel", b =>
                {
                    b.HasOne("ParcelMicroservice.Entities.Parcel", "Parcel")
                        .WithMany()
                        .HasForeignKey("ParcelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcel");
                });
#pragma warning restore 612, 618
        }
    }
}
