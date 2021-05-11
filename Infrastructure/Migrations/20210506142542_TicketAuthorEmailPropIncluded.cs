using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class TicketAuthorEmailPropIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketAuthor",
                table: "Tickets",
                newName: "TicketAuthorId");

            migrationBuilder.AddColumn<string>(
                name: "TicketAuthorEmail",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketAuthorEmail",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketAuthorId",
                table: "Tickets",
                newName: "TicketAuthor");
        }
    }
}
