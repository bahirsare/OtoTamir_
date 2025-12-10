using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Billing_date_fix : Migration
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
                name: "DueDate",
                table: "BankCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "BillingDate",
                table: "BankCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "BankCards",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BillingDate",
                table: "BankCards",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
