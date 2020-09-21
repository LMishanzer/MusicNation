using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicNation.Migrations
{
    public partial class IdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdOnDrive",
                table: "Songs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOnDrive",
                table: "Songs");
        }
    }
}
