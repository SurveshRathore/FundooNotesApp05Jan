using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class Notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTable_UserTable_userId",
                table: "NoteTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteTable",
                table: "NoteTable");

            migrationBuilder.RenameTable(
                name: "NoteTable",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_NoteTable_userId",
                table: "Notes",
                newName: "IX_Notes_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_UserTable_userId",
                table: "Notes",
                column: "userId",
                principalTable: "UserTable",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_UserTable_userId",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "NoteTable");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_userId",
                table: "NoteTable",
                newName: "IX_NoteTable_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteTable",
                table: "NoteTable",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTable_UserTable_userId",
                table: "NoteTable",
                column: "userId",
                principalTable: "UserTable",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
