using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class StudentTablesm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTables",
                table: "StudentTables");

            migrationBuilder.RenameTable(
                name: "StudentTables",
                newName: "StudentTable");

            migrationBuilder.AddColumn<int>(
                name: "StudentMobile",
                table: "StudentTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTable",
                table: "StudentTable",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTable",
                table: "StudentTable");

            migrationBuilder.DropColumn(
                name: "StudentMobile",
                table: "StudentTable");

            migrationBuilder.RenameTable(
                name: "StudentTable",
                newName: "StudentTables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTables",
                table: "StudentTables",
                column: "StudentId");
        }
    }
}
