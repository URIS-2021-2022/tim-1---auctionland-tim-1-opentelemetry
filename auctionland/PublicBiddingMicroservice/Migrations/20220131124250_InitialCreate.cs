using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicBiddingMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceIncrease = table.Column<double>(type: "float", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.AuctionId);
                });

            migrationBuilder.CreateTable(
                name: "PublicBiddings",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartingPricePerHe = table.Column<double>(type: "float", nullable: false),
                    Exclude = table.Column<bool>(type: "bit", nullable: false),
                    AuctionedPrice = table.Column<double>(type: "float", nullable: false),
                    LeasePeriod = table.Column<int>(type: "int", nullable: false),
                    NumberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    DepositReplenishment = table.Column<double>(type: "float", nullable: false),
                    Circle = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBiddings", x => x.PublicBiddingId);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.StageId);
                });

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "AuctionId", "ApplicationDeadline", "Date", "Number", "PriceIncrease", "Year" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 10.0, 2021 });

            migrationBuilder.InsertData(
                table: "PublicBiddings",
                columns: new[] { "PublicBiddingId", "AuctionedPrice", "Circle", "Date", "DepositReplenishment", "EndTime", "Exclude", "LeasePeriod", "NumberOfParticipants", "StartTime", "StartingPricePerHe", "Status" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 139.0, 1, new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 13.0, new DateTime(2020, 11, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 12, new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 10.0, "Status1" });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "StageId", "Date" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "PublicBiddings");

            migrationBuilder.DropTable(
                name: "Stages");
        }
    }
}
