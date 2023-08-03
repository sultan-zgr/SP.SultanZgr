using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Data.Migrations
{
    public partial class m45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuesAmount",
                table: "MonthlyInvoices");

            migrationBuilder.DropColumn(
                name: "ElectricityBill",
                table: "MonthlyInvoices");

            migrationBuilder.DropColumn(
                name: "GasBill",
                table: "MonthlyInvoices");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "InvoiceAmount");

            migrationBuilder.RenameColumn(
                name: "WaterBill",
                table: "MonthlyInvoices",
                newName: "InvoiceAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceAmount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "InvoiceAmount",
                table: "MonthlyInvoices",
                newName: "WaterBill");

            migrationBuilder.AddColumn<decimal>(
                name: "DuesAmount",
                table: "MonthlyInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ElectricityBill",
                table: "MonthlyInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GasBill",
                table: "MonthlyInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
