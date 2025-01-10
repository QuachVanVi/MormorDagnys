using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MormorsBageri.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStockToSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "SupplierProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "SupplierProducts");
        }
    }
}
