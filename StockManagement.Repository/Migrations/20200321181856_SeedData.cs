using Microsoft.EntityFrameworkCore.Migrations;

namespace StockManagement.Repository.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "Price", "Status", "StockQuantity" },
                values: new object[] { 1, "Apple Inc", 50, true, 100 });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "Price", "Status", "StockQuantity" },
                values: new object[] { 2, "Google Inc", 150, true, 200 });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "Price", "Status", "StockQuantity" },
                values: new object[] { 3, "Microsoft Inc", 100, true, 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: 3);
        }
    }
}
