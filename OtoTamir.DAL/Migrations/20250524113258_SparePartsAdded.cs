using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SparePartsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_ServiceRecords_ServiceRecordId",
                table: "Symptom");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_Vehicles_VehicleId",
                table: "Symptom");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Symptom",
                table: "Symptom");

            migrationBuilder.DropIndex(
                name: "IX_Symptom_VehicleId",
                table: "Symptom");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Symptom");

            migrationBuilder.RenameTable(
                name: "Symptom",
                newName: "Symptoms");

            migrationBuilder.RenameIndex(
                name: "IX_Symptom_ServiceRecordId",
                table: "Symptoms",
                newName: "IX_Symptoms_ServiceRecordId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "Symptoms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDaysToFix",
                table: "Symptoms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Symptoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PossibleSolution",
                table: "Symptoms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Symptoms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Symptoms",
                table: "Symptoms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RepairComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairComments_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompatibleVehicles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SymptomId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpareParts_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_SparePartId",
                table: "PurchaseDetails",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairComments_SymptomId",
                table: "RepairComments",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_SymptomId",
                table: "SpareParts",
                column: "SymptomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_ServiceRecords_ServiceRecordId",
                table: "Symptoms",
                column: "ServiceRecordId",
                principalTable: "ServiceRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_ServiceRecords_ServiceRecordId",
                table: "Symptoms");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "RepairComments");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Symptoms",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "EstimatedDaysToFix",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "PossibleSolution",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Symptoms");

            migrationBuilder.RenameTable(
                name: "Symptoms",
                newName: "Symptom");

            migrationBuilder.RenameIndex(
                name: "IX_Symptoms_ServiceRecordId",
                table: "Symptom",
                newName: "IX_Symptom_ServiceRecordId");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Symptom",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Symptom",
                table: "Symptom",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Symptom_VehicleId",
                table: "Symptom",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptom_ServiceRecords_ServiceRecordId",
                table: "Symptom",
                column: "ServiceRecordId",
                principalTable: "ServiceRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptom_Vehicles_VehicleId",
                table: "Symptom",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
