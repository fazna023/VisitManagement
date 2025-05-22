using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorManagement.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitLogs_Employees_HostEmployeeId",
                table: "VisitLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitLogs_Visitors_VisitorId",
                table: "VisitLogs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Visitors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Visitors",
                newName: "ContactInfo");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "Visitors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CheckinTime",
                table: "VisitLogs",
                newName: "CheckInTime");

            migrationBuilder.RenameColumn(
                name: "VisitId",
                table: "VisitLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledTime",
                table: "VisitLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "VisitLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutTime",
                table: "VisitLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitLogs_Employees_HostEmployeeId",
                table: "VisitLogs",
                column: "HostEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitLogs_Visitors_VisitorId",
                table: "VisitLogs",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitLogs_Employees_HostEmployeeId",
                table: "VisitLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitLogs_Visitors_VisitorId",
                table: "VisitLogs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Visitors",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Visitors",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Visitors",
                newName: "VisitorId");

            migrationBuilder.RenameColumn(
                name: "CheckInTime",
                table: "VisitLogs",
                newName: "CheckinTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VisitLogs",
                newName: "VisitId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employees",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ScheduledTime",
                table: "VisitLogs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckOutTime",
                table: "VisitLogs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CheckinTime",
                table: "VisitLogs",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitLogs_Employees_HostEmployeeId",
                table: "VisitLogs",
                column: "HostEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitLogs_Visitors_VisitorId",
                table: "VisitLogs",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
