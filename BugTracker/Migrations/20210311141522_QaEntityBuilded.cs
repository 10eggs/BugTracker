using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class QaEntityBuilded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QaID",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketCategory",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketPriority",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketStatus",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updated",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QAId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QA", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_QaID",
                table: "Tickets",
                column: "QaID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_QAId",
                table: "Project",
                column: "QAId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_QA_QAId",
                table: "Project",
                column: "QAId",
                principalTable: "QA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_QA_QaID",
                table: "Tickets",
                column: "QaID",
                principalTable: "QA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_QA_QAId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_QA_QaID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "QA");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_QaID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Project_QAId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "QaID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketCategory",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketPriority",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketStatus",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "QAId",
                table: "Project");
        }
    }
}
