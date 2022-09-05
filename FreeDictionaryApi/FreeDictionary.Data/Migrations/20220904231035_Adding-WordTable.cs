using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeDictionary.Data.Migrations
{
    public partial class AddingWordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteWord_Users_UserId",
                table: "FavoriteWord");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryWord_Users_UserId",
                table: "HistoryWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryWord",
                table: "HistoryWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteWord",
                table: "FavoriteWord");

            migrationBuilder.RenameTable(
                name: "HistoryWord",
                newName: "HistoryWords");

            migrationBuilder.RenameTable(
                name: "FavoriteWord",
                newName: "FavoriteWords");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryWord_UserId",
                table: "HistoryWords",
                newName: "IX_HistoryWords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteWord_UserId",
                table: "FavoriteWords",
                newName: "IX_FavoriteWords_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryWords",
                table: "HistoryWords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteWords",
                table: "FavoriteWords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteWords_Users_UserId",
                table: "FavoriteWords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryWords_Users_UserId",
                table: "HistoryWords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteWords_Users_UserId",
                table: "FavoriteWords");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryWords_Users_UserId",
                table: "HistoryWords");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryWords",
                table: "HistoryWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteWords",
                table: "FavoriteWords");

            migrationBuilder.RenameTable(
                name: "HistoryWords",
                newName: "HistoryWord");

            migrationBuilder.RenameTable(
                name: "FavoriteWords",
                newName: "FavoriteWord");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryWords_UserId",
                table: "HistoryWord",
                newName: "IX_HistoryWord_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteWords_UserId",
                table: "FavoriteWord",
                newName: "IX_FavoriteWord_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryWord",
                table: "HistoryWord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteWord",
                table: "FavoriteWord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteWord_Users_UserId",
                table: "FavoriteWord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryWord_Users_UserId",
                table: "HistoryWord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
