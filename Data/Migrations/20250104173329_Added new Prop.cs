using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MormorsBageri.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddednewProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProducts_SupplierId",
                table: "SupplierProducts",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierProducts_Products_ProductId",
                table: "SupplierProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierProducts_Suppliers_SupplierId",
                table: "SupplierProducts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierProducts_Products_ProductId",
                table: "SupplierProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierProducts_Suppliers_SupplierId",
                table: "SupplierProducts");

            migrationBuilder.DropIndex(
                name: "IX_SupplierProducts_SupplierId",
                table: "SupplierProducts");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");
        }
    }
}
