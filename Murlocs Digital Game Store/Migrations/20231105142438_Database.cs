using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGameStore.Migrations
{
    /// <inheritdoc />
    public partial class Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Genre_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Genre_ID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Publisher_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Publisher_ID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Game_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    PublisherID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Game_Id);
                    table.ForeignKey(
                        name: "FK_Game_Publisher_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publisher",
                        principalColumn: "Publisher_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    GameGenres_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Game_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    GamesGame_Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Genre_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    GenresGenre_ID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => x.GameGenres_ID);
                    table.ForeignKey(
                        name: "FK_GameGenres_Game_GamesGame_Id",
                        column: x => x.GamesGame_Id,
                        principalTable: "Game",
                        principalColumn: "Game_Id");
                    table.ForeignKey(
                        name: "FK_GameGenres_Genre_GenresGenre_ID",
                        column: x => x.GenresGenre_ID,
                        principalTable: "Genre",
                        principalColumn: "Genre_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublisherID",
                table: "Game",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GamesGame_Id",
                table: "GameGenres",
                column: "GamesGame_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenresGenre_ID",
                table: "GameGenres",
                column: "GenresGenre_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
