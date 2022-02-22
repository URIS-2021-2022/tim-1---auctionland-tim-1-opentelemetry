using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizedPerson",
                columns: table => new
                {
                    AuthorizedPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPersonalDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardTable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPerson", x => x.AuthorizedPersonID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizedPersonCustomer",
                columns: table => new
                {
                    AuthorizedPersonCustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorizedPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPersonCustomer", x => x.AuthorizedPersonCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "LegallyPerson",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPhysicalPerson = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealizedArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasABan = table.Column<bool>(type: "bit", nullable: false),
                    StartDateBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationBan = table.Column<int>(type: "int", nullable: false),
                    EndDateBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegallyPerson", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPerson",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPhysicalPerson = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealizedArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasABan = table.Column<bool>(type: "bit", nullable: false),
                    StartDateBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationBan = table.Column<int>(type: "int", nullable: false),
                    EndDateBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPerson", x => x.CustomerID);
                });

            migrationBuilder.InsertData(
                table: "AuthorizedPerson",
                columns: new[] { "AuthorizedPersonID", "AddressId", "BoardTable", "FirstName", "LastName", "NumberPersonalDocument" },
                values: new object[,]
                {
                    { new Guid("4a151b19-a287-47f7-61bd-4a596c397461"), new Guid("2a151b12-a287-47f2-61bd-4a596c397461"), "1", "Nikola", "Nikolic", "012345788" },
                    { new Guid("4b151b29-a287-47f7-61bd-2a596c397431"), new Guid("3a151b32-a287-47f3-61bd-4a596c397463"), "2", "Nemanja", "Nenadovic", "025445739" }
                });

            migrationBuilder.InsertData(
                table: "AuthorizedPersonCustomer",
                columns: new[] { "AuthorizedPersonCustomerID", "AuthorizedPersonID", "CustomerID" },
                values: new object[] { new Guid("5b151b29-a285-47f7-61bd-2a596c397425"), new Guid("4a151b19-a287-47f7-61bd-4a596c397461"), new Guid("2a411c13-a196-48f7-88bd-67596c3974c3") });

            migrationBuilder.InsertData(
                table: "LegallyPerson",
                columns: new[] { "CustomerID", "AccountNumber", "AddressId", "DocumentID", "DurationBan", "Email", "EndDateBan", "Fax", "HasABan", "IdentificationNumber", "IsPhysicalPerson", "Name", "PhoneNumber1", "PhoneNumber2", "Priority", "RealizedArea", "StartDateBan" },
                values: new object[] { new Guid("4a411c13-a196-48f7-88bd-67596c3974c4"), "3204568885231", new Guid("5b411c15-a196-48f7-88bd-67596c3974c5"), new Guid("6a411c13-6196-48f7-88bd-57596c3974b6"), 1, "context@info.com", new DateTime(2021, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), "021352664", true, "325687964", false, "Context", "066451235", "0643214458", "Visok", "38ha", new DateTime(2020, 11, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PhysicalPerson",
                columns: new[] { "CustomerID", "AccountNumber", "AddressId", "DocumentID", "DurationBan", "Email", "EndDateBan", "FirstName", "HasABan", "IsPhysicalPerson", "JMBG", "LastName", "PhoneNumber1", "PhoneNumber2", "Priority", "RealizedArea", "StartDateBan" },
                values: new object[] { new Guid("2a411c13-a196-48f7-88bd-67596c3974c3"), "36105666854456", new Guid("1b411c11-a196-48f7-88bd-67596c3974c1"), new Guid("3a411c13-a196-48f7-88bd-57596c3974b3"), 3, "marko@gmail.com", new DateTime(2021, 10, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), "Marko", true, true, "1106985770036", "Markovic", "0652365574", "0632456685", "Nizak", "10ha", new DateTime(2018, 10, 13, 9, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizedPerson");

            migrationBuilder.DropTable(
                name: "AuthorizedPersonCustomer");

            migrationBuilder.DropTable(
                name: "LegallyPerson");

            migrationBuilder.DropTable(
                name: "PhysicalPerson");
        }
    }
}
