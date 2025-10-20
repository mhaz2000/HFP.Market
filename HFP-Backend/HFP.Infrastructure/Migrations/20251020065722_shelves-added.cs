using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HFP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shelvesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShelfId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShelfId",
                table: "Products",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shelves_ShelfId",
                table: "Products",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shelves_ShelfId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShelfId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "Products");
        }
    }
}
