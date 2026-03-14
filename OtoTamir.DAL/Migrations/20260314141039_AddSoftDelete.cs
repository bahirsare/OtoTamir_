using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Treasuries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TransactionCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "TransactionCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TransactionCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TransactionCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Symptoms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SpareParts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServiceRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RepairComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PosTerminals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Banks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BankCards",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Treasuries");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TransactionCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RepairComments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PosTerminals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BankCards");
        }
    }
}
