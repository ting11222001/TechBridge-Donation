using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBridgeDonation.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameAssignedPartnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Organisations_AssignedPartnerId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "AssignedPartnerId",
                table: "Devices",
                newName: "AssignedRefurbPartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_AssignedPartnerId",
                table: "Devices",
                newName: "IX_Devices_AssignedRefurbPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Organisations_AssignedRefurbPartnerId",
                table: "Devices",
                column: "AssignedRefurbPartnerId",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Organisations_AssignedRefurbPartnerId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "AssignedRefurbPartnerId",
                table: "Devices",
                newName: "AssignedPartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_AssignedRefurbPartnerId",
                table: "Devices",
                newName: "IX_Devices_AssignedPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Organisations_AssignedPartnerId",
                table: "Devices",
                column: "AssignedPartnerId",
                principalTable: "Organisations",
                principalColumn: "Id");
        }
    }
}
