using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDiscGolf.Models.Migrations
{
    public partial class editFriendsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FriendRequestAccepted",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendRequestAccepted",
                table: "Friends");
        }
    }
}
