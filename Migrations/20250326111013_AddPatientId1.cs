using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easymed_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_PatientId1",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PatientId1",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Appointment");

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId",
                table: "Patient",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment");

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
    }
}
