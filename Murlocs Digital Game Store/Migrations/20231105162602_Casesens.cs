using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGameStore.Migrations
{
    /// <inheritdoc />
    public partial class Casesens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Genre_GenresGenre_ID",
                table: "GameGenres");

            migrationBuilder.RenameColumn(
                name: "Publisher_ID",
                table: "Publisher",
                newName: "Publisher_Id");

            migrationBuilder.RenameColumn(
                name: "Interest_ID",
                table: "InterestTable",
                newName: "Interest_Id");

            migrationBuilder.RenameColumn(
                name: "Genre_ID",
                table: "Genre",
                newName: "Genre_Id");

            migrationBuilder.RenameColumn(
                name: "GenresGenre_ID",
                table: "GameGenres",
                newName: "GenresGenre_Id");

            migrationBuilder.RenameColumn(
                name: "Genre_ID",
                table: "GameGenres",
                newName: "Genre_Id");

            migrationBuilder.RenameColumn(
                name: "Game_ID",
                table: "GameGenres",
                newName: "Game_Id");

            migrationBuilder.RenameColumn(
                name: "GameGenres_ID",
                table: "GameGenres",
                newName: "GameGenres_Id");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenres_GenresGenre_ID",
                table: "GameGenres",
                newName: "IX_GameGenres_GenresGenre_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Genre_GenresGenre_Id",
                table: "GameGenres",
                column: "GenresGenre_Id",
                principalTable: "Genre",
                principalColumn: "Genre_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Genre_GenresGenre_Id",
                table: "GameGenres");

            migrationBuilder.RenameColumn(
                name: "Publisher_Id",
                table: "Publisher",
                newName: "Publisher_ID");

            migrationBuilder.RenameColumn(
                name: "Interest_Id",
                table: "InterestTable",
                newName: "Interest_ID");

            migrationBuilder.RenameColumn(
                name: "Genre_Id",
                table: "Genre",
                newName: "Genre_ID");

            migrationBuilder.RenameColumn(
                name: "GenresGenre_Id",
                table: "GameGenres",
                newName: "GenresGenre_ID");

            migrationBuilder.RenameColumn(
                name: "Genre_Id",
                table: "GameGenres",
                newName: "Genre_ID");

            migrationBuilder.RenameColumn(
                name: "Game_Id",
                table: "GameGenres",
                newName: "Game_ID");

            migrationBuilder.RenameColumn(
                name: "GameGenres_Id",
                table: "GameGenres",
                newName: "GameGenres_ID");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenres_GenresGenre_Id",
                table: "GameGenres",
                newName: "IX_GameGenres_GenresGenre_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Genre_GenresGenre_ID",
                table: "GameGenres",
                column: "GenresGenre_ID",
                principalTable: "Genre",
                principalColumn: "Genre_ID");
        }
    }
}
