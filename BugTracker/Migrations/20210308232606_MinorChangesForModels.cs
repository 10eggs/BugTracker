using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class MinorChangesForModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectOwner_ProjectOwnerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectOwnerId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ProjectOwner");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "ProjectOwner");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "ProjectOwner");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ProjectOwner");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "ProjectOwner");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectOwnerId",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectOwnerId1",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectOwnerId1",
                table: "Project",
                column: "ProjectOwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectOwner_ProjectOwnerId1",
                table: "Project",
                column: "ProjectOwnerId1",
                principalTable: "ProjectOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectOwner_ProjectOwnerId1",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectOwnerId1",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectOwnerId1",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ProjectOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "ProjectOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "ProjectOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ProjectOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "ProjectOwner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectOwnerId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectOwnerId",
                table: "Project",
                column: "ProjectOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectOwner_ProjectOwnerId",
                table: "Project",
                column: "ProjectOwnerId",
                principalTable: "ProjectOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
