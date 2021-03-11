using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class QAsPropertyAddedToTheProjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_QA_QAId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_QAId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "QAId",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "ProjectQA",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    QAsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectQA", x => new { x.ProjectsId, x.QAsId });
                    table.ForeignKey(
                        name: "FK_ProjectQA_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectQA_QA_QAsId",
                        column: x => x.QAsId,
                        principalTable: "QA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQA_QAsId",
                table: "ProjectQA",
                column: "QAsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectQA");

            migrationBuilder.AddColumn<int>(
                name: "QAId",
                table: "Project",
                type: "int",
                nullable: true);

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
        }
    }
}
