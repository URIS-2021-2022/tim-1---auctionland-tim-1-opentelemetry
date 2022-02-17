using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "CityID", "CityName", "CountryID", "CountryName", "Number", "Street", "ZipCode" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "Veternik", new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Srbija", 89, "Devet Jugovica", "21203" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "CityID", "CityName", "CountryID", "CountryName", "Number", "Street", "ZipCode" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), "Veternik", new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Srbija", 2, "Svetozara Miletica", "21203" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
