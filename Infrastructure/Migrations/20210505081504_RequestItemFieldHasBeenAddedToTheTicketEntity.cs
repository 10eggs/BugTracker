using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RequestItemFieldHasBeenAddedToTheTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestItemId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RequestItemId",
                table: "Tickets",
                column: "RequestItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Requests_RequestItemId",
                table: "Tickets",
                column: "RequestItemId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Requests_RequestItemId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_RequestItemId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RequestItemId",
                table: "Tickets");
        }
    }
}
