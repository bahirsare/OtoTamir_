using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoTamir.DAL.Migrations
{
    /// <inheritdoc />
    public partial class servicerecordsymptomRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_Vehicles_VehicleId",
                table: "Symptom");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Symptom",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceRecordId",
                table: "Symptom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Symptom_ServiceRecordId",
                table: "Symptom",
                column: "ServiceRecordId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_ServiceRecords_ServiceRecordId",
                table: "Symptom");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptom_Vehicles_VehicleId",
                table: "Symptom");

            migrationBuilder.DropIndex(
                name: "IX_Symptom_ServiceRecordId",
                table: "Symptom");

            migrationBuilder.DropColumn(
                name: "ServiceRecordId",
                table: "Symptom");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Symptom",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptom_Vehicles_VehicleId",
                table: "Symptom",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
