using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HFP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactions_Products_ProductId",
                table: "ProductTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactions_Transactions_TransactionId",
                table: "ProductTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactions_Products_ProductId",
                table: "ProductTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactions_Transactions_TransactionId",
                table: "ProductTransactions",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactions_Products_ProductId",
                table: "ProductTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactions_Transactions_TransactionId",
                table: "ProductTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactions_Products_ProductId",
                table: "ProductTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactions_Transactions_TransactionId",
                table: "ProductTransactions",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
