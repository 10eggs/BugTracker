using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class AssignedTicketEntityRemovedFromDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TicketStatus",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Open",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Open");

            migrationBuilder.AlterColumn<string>(
                name: "TicketPriority",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Low");

            migrationBuilder.AlterColumn<string>(
                name: "TicketCategory",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "None",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Other");

            migrationBuilder.AlterColumn<int>(
                name: "QaID",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Tickets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "TicketStatus",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Open",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Open");

            migrationBuilder.AlterColumn<string>(
                name: "TicketPriority",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Low",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "TicketCategory",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Other",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "None");

            migrationBuilder.AlterColumn<int>(
                name: "QaID",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
