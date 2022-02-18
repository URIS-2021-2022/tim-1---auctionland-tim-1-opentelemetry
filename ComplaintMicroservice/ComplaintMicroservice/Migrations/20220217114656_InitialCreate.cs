using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfDecision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfDecision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplaintTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplaintStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintId);
                });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ComplaintId", "Action", "ComplaintStatus", "ComplaintStatusId", "ComplaintTypeId", "ComplaintTypeName", "DateOfDecision", "Explanation", "NumberOfDecision", "Reason", "SubmissionDate" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Public bidding does not go to the second round", "Rejected", new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"), new Guid("21ad52f8-0281-4241-98b0-481566d25e4f"), "Appeal against the course of the public tender", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "Explanation", "1", "Reason", new DateTime(2020, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "ComplaintId", "Action", "ComplaintStatus", "ComplaintStatusId", "ComplaintTypeId", "ComplaintTypeName", "DateOfDecision", "Explanation", "NumberOfDecision", "Reason", "SubmissionDate" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Public bidding does not go to the second round", "Rejected", new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"), new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"), "Appeal against the Decision on Leasing", new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "Explanation", "2", "Reason", new DateTime(2020, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");
        }
    }
}
