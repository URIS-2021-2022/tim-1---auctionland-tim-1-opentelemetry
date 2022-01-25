using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ListOfDocuments");
        }
    }
}
