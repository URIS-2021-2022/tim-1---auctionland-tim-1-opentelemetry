using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMicroservice.Migrations
{
    /// <summary>
    /// Kreirana particiona klasa InitialCreate
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Metoda za kreiranje i punjenje tabele Ads podacima
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads", 
                columns: table => new 
                {
                    AdID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MunicipalityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialListNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdID);
                });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "AdID", "AdName", "DateOfIssue", "MunicipalityName", "OfficialListID", "OfficialListNumber" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Prodaja placa kod pijace", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "Čantavir", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "1/2022" });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "AdID", "AdName", "DateOfIssue", "MunicipalityName", "OfficialListID", "OfficialListNumber" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Prodaja placa pored crkve", new DateTime(2020, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), "Žednik", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), "2/2022" });
        }

        /// <summary>
        /// Metoda za brisanje tabele Ads
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
