using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicBiddingRegistrationMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationForPublicBidding",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForPublicBidding", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentForApplications",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: false),
                    PurposeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentForApplications", x => x.PaymentId);
                });

            migrationBuilder.InsertData(
                table: "ApplicationForPublicBidding",
                columns: new[] { "ApplicationId", "BuyerId", "PaymentId" },
                values: new object[] { new Guid("a4927ae1-52e0-4b8e-bcfc-9b353cb3a7e1"), new Guid("ab91c3fc-e095-4c38-b2a6-4117de3ac0b9"), new Guid("cf72ddaf-66ff-435c-8129-940b25ad943d") });

            migrationBuilder.InsertData(
                table: "PaymentForApplications",
                columns: new[] { "PaymentId", "AccountNumber", "DateOfPayment", "PublicBiddingId", "PurposeOfPayment", "ReferenceNumber" },
                values: new object[] { new Guid("8ac08292-9406-45cf-aff4-0c9447880779"), 154564, new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7344fd85-ab0e-4da3-9fa4-d65af38e001f"), "Some purpose", 855464 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationForPublicBidding");

            migrationBuilder.DropTable(
                name: "PaymentForApplications");
        }
    }
}
