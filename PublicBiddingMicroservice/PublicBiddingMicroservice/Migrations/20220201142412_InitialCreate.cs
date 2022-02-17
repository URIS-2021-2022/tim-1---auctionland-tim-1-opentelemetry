﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicBiddingMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageId);
                });

            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceIncrease = table.Column<double>(type: "float", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.AuctionId);
                    table.ForeignKey(
                        name: "FK_Auction_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicBidding",
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBidding", x => x.PublicBiddingId);
                    table.ForeignKey(
                        name: "FK_PublicBidding_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Stage",
                columns: new[] { "StageId", "Date" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Auction",
                columns: new[] { "AuctionId", "ApplicationDeadline", "Date", "Number", "PriceIncrease", "StageId", "Year" },
                values: new object[] { new Guid("4c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 10.0, new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), 2021 });

            migrationBuilder.InsertData(
                table: "PublicBidding",
                columns: new[] { "PublicBiddingId", "AuctionedPrice", "Circle", "Date", "DepositReplenishment", "EndTime", "Exclude", "LeasePeriod", "NumberOfParticipants", "StageId", "StartTime", "StartingPricePerHe", "Status" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), 139.0, 1, new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 13.0, new DateTime(2020, 11, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 12, new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 10.0, "Status1" });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_StageId",
                table: "Auction",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicBidding_StageId",
                table: "PublicBidding",
                column: "StageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auction");

            migrationBuilder.DropTable(
                name: "PublicBidding");

            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
