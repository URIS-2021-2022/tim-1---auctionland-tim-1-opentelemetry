using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommissionMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    CommissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameCommission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.CommissionID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameCommission = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Members_Commissions_CommissionID",
                        column: x => x.CommissionID,
                        principalTable: "Commissions",
                        principalColumn: "CommissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "CommissionID", "NameCommission", "UserId" },
                values: new object[] { new Guid("8a411c13-a195-48f7-8dbd-67596c3974c0"), "Prva komisija", new Guid("7a411c13-a295-48f7-8dbd-68596c3974c0") });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "CommissionID", "NameCommission", "UserId" },
                values: new object[] { new Guid("8a211c13-a195-48f7-8dbd-67596c3974c0"), "Druga komisija", new Guid("5a411c13-a295-44f7-8dbd-68596c3974c1") });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "CommissionID", "FirstName", "LastName", "NameCommission", "Role" },
                values: new object[] { new Guid("2a411c13-a196-48f7-88bd-67596c3974c3"), new Guid("8a411c13-a195-48f7-8dbd-67596c3974c0"), "Marko", "Markovic", "Prva komisija", "Predsednik" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "CommissionID", "FirstName", "LastName", "NameCommission", "Role" },
                values: new object[] { new Guid("3a411c13-a196-48f7-88bd-67596c3974c2"), new Guid("8a411c13-a195-48f7-8dbd-67596c3974c0"), "Petar", "Petrovic", "Prva komisija", "Clan" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "CommissionID", "FirstName", "LastName", "NameCommission", "Role" },
                values: new object[] { new Guid("4a411c13-a196-48f7-88bd-67596c3974c4"), new Guid("8a411c13-a195-48f7-8dbd-67596c3974c0"), "Jovan", "Jovanovic", "Prva komisija", "Clan" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_CommissionID",
                table: "Members",
                column: "CommissionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Commissions");
        }
    }
}
