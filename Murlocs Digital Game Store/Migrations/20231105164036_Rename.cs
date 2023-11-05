using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGameStore.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestTable");

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    Interest_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.Interest_Id);
                    table.ForeignKey(
                        name: "FK_Interest_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "Game_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interest_GameID",
                table: "Interest",
                column: "GameID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.CreateTable(
                name: "InterestTable",
                columns: table => new
                {
                    Interest_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestTable", x => x.Interest_Id);
                    table.ForeignKey(
                        name: "FK_InterestTable_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "Game_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestTable_GameID",
                table: "InterestTable",
                column: "GameID");
        }
    }
}
