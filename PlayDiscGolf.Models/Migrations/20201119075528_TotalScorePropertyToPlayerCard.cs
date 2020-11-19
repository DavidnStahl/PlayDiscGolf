using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDiscGolf.Models.Migrations
{
    public partial class TotalScorePropertyToPlayerCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalScore",
                table: "PlayerCards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "PlayerCards");
        }
    }
}
