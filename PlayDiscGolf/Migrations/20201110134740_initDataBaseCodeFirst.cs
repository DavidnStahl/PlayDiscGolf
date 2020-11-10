using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDiscGolf.Migrations
{
    public partial class initDataBaseCodeFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "ScoreCards",
                columns: table => new
                {
                    ScoreCardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreCards", x => x.ScoreCardID);
                    table.ForeignKey(
                        name: "FK_ScoreCards_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCards",
                columns: table => new
                {
                    PlayerCardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ScoreCardID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCards", x => x.PlayerCardID);
                    table.ForeignKey(
                        name: "FK_PlayerCards_ScoreCards_ScoreCardID",
                        column: x => x.ScoreCardID,
                        principalTable: "ScoreCards",
                        principalColumn: "ScoreCardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoleCards",
                columns: table => new
                {
                    HoleCardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoleNumber = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    PlayerCardID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoleCards", x => x.HoleCardID);
                    table.ForeignKey(
                        name: "FK_HoleCards_PlayerCards_PlayerCardID",
                        column: x => x.PlayerCardID,
                        principalTable: "PlayerCards",
                        principalColumn: "PlayerCardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoleCards_PlayerCardID",
                table: "HoleCards",
                column: "PlayerCardID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_ScoreCardID",
                table: "PlayerCards",
                column: "ScoreCardID");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreCards_CourseID",
                table: "ScoreCards",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoleCards");

            migrationBuilder.DropTable(
                name: "PlayerCards");

            migrationBuilder.DropTable(
                name: "ScoreCards");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
