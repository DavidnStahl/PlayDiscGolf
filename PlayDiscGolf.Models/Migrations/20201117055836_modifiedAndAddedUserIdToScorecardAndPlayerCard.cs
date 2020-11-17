using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDiscGolf.Models.Migrations
{
    public partial class modifiedAndAddedUserIdToScorecardAndPlayerCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlayerCards",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "ScoreCards",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "PlayerCards",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "PlayerCards");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "PlayerCards",
                newName: "Name");
        }
    }
}
