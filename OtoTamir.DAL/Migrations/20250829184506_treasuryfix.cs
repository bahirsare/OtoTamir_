using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class treasuryfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Treasuries_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TreasuryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TreasuryId",
                table: "AspNetUsers",
                column: "TreasuryId",
                unique: true,
                filter: "[TreasuryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Treasuries_TreasuryId",
                table: "AspNetUsers",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Treasuries_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TreasuryId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TreasuryId",
                table: "AspNetUsers",
                column: "TreasuryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Treasuries_TreasuryId",
                table: "AspNetUsers",
                column: "TreasuryId",
                principalTable: "Treasuries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
