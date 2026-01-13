using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class pagingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MechanicId",
                table: "TransactionCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MechanicId",
                table: "TransactionCategories");
        }
    }
}
