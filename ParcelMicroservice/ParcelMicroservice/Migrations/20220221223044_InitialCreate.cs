using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParcelMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    ParcelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurfaceArea = table.Column<int>(type: "int", nullable: false),
                    NumberOfParcel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateListNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CultureRealStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassRealStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkabilityActualStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProtectedZoneActualStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormOfOwnership = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrainageActualCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MunicipalityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfTheMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.ParcelID);
                });

            migrationBuilder.CreateTable(
                name: "PartOfParcels",
                columns: table => new
                {
                    ParcelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartOfParcelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurfaceAreaPOP = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassLandLabel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfParcels", x => new { x.ParcelID, x.PartOfParcelID });
                    table.ForeignKey(
                        name: "FK_PartOfParcels_Parcels_ParcelID",
                        column: x => x.ParcelID,
                        principalTable: "Parcels",
                        principalColumn: "ParcelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parcels",
                columns: new[] { "ParcelID", "ClassRealStatus", "CultureRealStatus", "DrainageActualCondition", "FormOfOwnership", "MunicipalityID", "NameOfTheMunicipality", "NumberOfParcel", "ProtectedZoneActualStatus", "RealEstateListNumber", "SurfaceArea", "WorkabilityActualStatus" },
                values: new object[] { new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"), "III", "Njive", "Površinsko odvodnjavanje", "Državna svojina", new Guid("e4902805-41f9-45d7-8b4d-7c85e8f27868"), "Čantavir", "1/2022", "2", "1 - Prepis", 6000, "Obradivo" });

            migrationBuilder.InsertData(
                table: "Parcels",
                columns: new[] { "ParcelID", "ClassRealStatus", "CultureRealStatus", "DrainageActualCondition", "FormOfOwnership", "MunicipalityID", "NameOfTheMunicipality", "NumberOfParcel", "ProtectedZoneActualStatus", "RealEstateListNumber", "SurfaceArea", "WorkabilityActualStatus" },
                values: new object[] { new Guid("628f7390-cb85-4b69-94bd-e3e6c424d725"), "II", "Pašnjaci", "Podzemno odvodnjavanje", "Društvena svojina", new Guid("aa3f2265-7182-4424-ba83-2eed388ce748"), "Bajmok", "2/2022", "1", "2 - Prepis", 3000, "Ostalo" });

            migrationBuilder.InsertData(
                table: "PartOfParcels",
                columns: new[] { "ParcelID", "PartOfParcelID", "ClassID", "ClassLandLabel", "SurfaceAreaPOP" },
                values: new object[] { new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"), new Guid("67c31d49-b189-4de5-a6e2-b9b5557047a9"), new Guid("61847780-396a-42a7-8e04-941e0d4eddf9"), "I", 1000 });

            migrationBuilder.InsertData(
                table: "PartOfParcels",
                columns: new[] { "ParcelID", "PartOfParcelID", "ClassID", "ClassLandLabel", "SurfaceAreaPOP" },
                values: new object[] { new Guid("866f2352-771f-4405-a9b5-9878b0fbff0f"), new Guid("17321524-7822-4daa-8134-a2ec4bed98e0"), new Guid("89e2bdc2-7153-463a-8c9f-37bfec240431"), "II", 500 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartOfParcels");

            migrationBuilder.DropTable(
                name: "Parcels");
        }
    }
}
