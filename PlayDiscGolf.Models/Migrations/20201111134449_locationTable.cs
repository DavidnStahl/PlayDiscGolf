using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDiscGolf.Migrations
{
    public partial class locationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HolesTotal",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Main",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalDistance",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalParValue",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Holes",
                columns: table => new
                {
                    HoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoleNumber = table.Column<int>(type: "int", nullable: false),
                    ParValue = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holes", x => x.HoleID);
                    table.ForeignKey(
                        name: "FK_Holes_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LocationID",
                table: "Courses",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Holes_CourseID",
                table: "Holes",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Locations_LocationID",
                table: "Courses",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Locations_LocationID",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Holes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LocationID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HolesTotal",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Main",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TotalDistance",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TotalParValue",
                table: "Courses");
        }
    }
}
