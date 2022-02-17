using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "UserTypeName" },
                values: new object[,]
                {
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Operater" },
                    { new Guid("1b7ea607-8ddb-493a-87fa-4bf5893e965b"), "Tehnički sekretar" },
                    { new Guid("9b7ea607-8ddb-493a-87fa-4bf5893e965b"), "Prva komisija" },
                    { new Guid("3c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Superuser" },
                    { new Guid("9a7ea607-8ddb-493a-87fa-4bf5893e965b"), "Operater Nadmetanja" },
                    { new Guid("7b7ea607-8ddb-493a-87fa-4bf5893e965b"), "Licitant" },
                    { new Guid("5c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Menadžer" },
                    { new Guid("3a7ea607-8ddb-493a-87fa-4bf5893e965b"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "Salt", "UserTypeId", "Username" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "petrović@gmail.com", "Petar", "Petrović", "12345", null, new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Petar123" });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserTypeId",
                table: "User",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
