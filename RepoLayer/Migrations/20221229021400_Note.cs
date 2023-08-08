using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLayer.Migrations
{
    public partial class Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteTable",
                columns: table => new
                {
                    NoteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteReminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoteColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteIsArchive = table.Column<bool>(type: "bit", nullable: true),
                    NoteIsPin = table.Column<bool>(type: "bit", nullable: true),
                    NoteIsTrash = table.Column<bool>(type: "bit", nullable: true),
                    NoteCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoteModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTable", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_NoteTable_UserTable_userId",
                        column: x => x.userId,
                        principalTable: "UserTable",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_userId",
                table: "NoteTable",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteTable");
        }
    }
}
