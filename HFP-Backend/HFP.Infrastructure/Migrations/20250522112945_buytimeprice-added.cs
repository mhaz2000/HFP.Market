using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HFP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class buytimepriceadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BuyTimePirce",
                table: "ProductTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BuyTimePurchasePirce",
                table: "ProductTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyTimePirce",
                table: "ProductTransactions");

            migrationBuilder.DropColumn(
                name: "BuyTimePurchasePirce",
                table: "ProductTransactions");
        }
    }
}
