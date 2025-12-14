using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class duedayfix : Migration
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

            migrationBuilder.DropColumn(
                name: "BillingDate",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "BankCards");

            migrationBuilder.AddColumn<int>(
                name: "BillingDay",
                table: "BankCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DueDay",
                table: "BankCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Treasuries_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TreasuryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingDay",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "DueDay",
                table: "BankCards");

            migrationBuilder.AddColumn<DateTime>(
                name: "BillingDate",
                table: "BankCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "BankCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
