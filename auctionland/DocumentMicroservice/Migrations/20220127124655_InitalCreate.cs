using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentMicroservice.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaseAgreements",
                columns: table => new
                {
                    LeaseAgreementID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50)", type: "nvarchar(max)", nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinisterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicBiddingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nvarchar100 = table.Column<string>(name: "nvarchar(100)", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseAgreements", x => x.LeaseAgreementID);
                });

            migrationBuilder.CreateTable(
                name: "ListOfDocuments",
                columns: table => new
                {
                    ListOfDocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfDocuments", x => x.ListOfDocumentsId);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    @int = table.Column<int>(name: "int", type: "int", nullable: false),
                    nvarchar250 = table.Column<string>(name: "nvarchar(250)", type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ListOfDocumentsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_ListOfDocuments_ListOfDocumentsID",
                        column: x => x.ListOfDocumentsID,
                        principalTable: "ListOfDocuments",
                        principalColumn: "ListOfDocumentsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LeaseAgreements",
                columns: new[] { "LeaseAgreementID", "datetime", "datetime", "datetime", "datetime", "nvarchar(50)", "MinisterID", "PersonID", "nvarchar(100)", "PublicBiddingID" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(2020, 11, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "FLease", new Guid("02daa4a7-7602-44e1-a0f0-f2bfeeceef5e"), new Guid("20d61140-9027-494f-a544-0112c3ee0f57"), "Some place", new Guid("f29575a1-37ce-4a94-998f-13e3119232fd") },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2020, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "SLease", new Guid("02daa4a7-7602-44e1-a0f0-f2bfeeceef5e"), new Guid("a4927ae1-52e0-4b8e-bcfc-9b353cb3a7e1"), "Second place", new Guid("f29575a1-37ce-4a94-998f-13e3119232fd") }
                });

            migrationBuilder.InsertData(
                table: "ListOfDocuments",
                columns: new[] { "ListOfDocumentsId", "ListCreationDate" },
                values: new object[,]
                {
                    { new Guid("9791d7f8-ee0e-4426-9bdb-2600d8aa8975"), new DateTime(2010, 11, 24, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7297a3c3-5674-4537-9ce2-1319de481a27"), new DateTime(2008, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "date", "nvarchar(250)", "int", "date", "nvarchar(250)", "ListOfDocumentsID" },
                values: new object[] { new Guid("8b44e760-3c55-4ae4-8d1b-c4ea053672ff"), new DateTime(2008, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "First", 1, new DateTime(2021, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "FTemplate", new Guid("9791d7f8-ee0e-4426-9bdb-2600d8aa8975") });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "date", "nvarchar(250)", "int", "date", "nvarchar(250)", "ListOfDocumentsID" },
                values: new object[] { new Guid("300f7843-4343-4520-ac1c-995d8095ea89"), new DateTime(2020, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Second", 2, new DateTime(2008, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "STemplate", new Guid("7297a3c3-5674-4537-9ce2-1319de481a27") });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ListOfDocumentsID",
                table: "Documents",
                column: "ListOfDocumentsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "LeaseAgreements");

            migrationBuilder.DropTable(
                name: "ListOfDocuments");
        }
    }
}
