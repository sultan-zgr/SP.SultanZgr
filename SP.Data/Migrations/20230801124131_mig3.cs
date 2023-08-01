using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Data.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Admins",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Admins",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Admins",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Admins",
                newName: "FirstName");
        }
    }
}
