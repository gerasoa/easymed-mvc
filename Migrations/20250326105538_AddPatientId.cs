using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easymed_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PatientId1",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId1",
                table: "Appointment",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_PatientId1",
                table: "Appointment",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_PatientId1",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PatientId1",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Appointment");
        }
    }
}
