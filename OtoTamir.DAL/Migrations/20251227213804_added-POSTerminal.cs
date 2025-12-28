using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedPOSTerminal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MaturityDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PosTerminalId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PosTerminals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaturityDays = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosTerminals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosTerminals_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PosTerminalId",
                table: "Transactions",
                column: "PosTerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_PosTerminals_BankId",
                table: "PosTerminals",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PosTerminals_PosTerminalId",
                table: "Transactions",
                column: "PosTerminalId",
                principalTable: "PosTerminals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PosTerminals_PosTerminalId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "PosTerminals");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PosTerminalId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "MaturityDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PosTerminalId",
                table: "Transactions");
        }
    }
}
