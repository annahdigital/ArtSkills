using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtSkills.Data.Migrations
{
    public partial class AppDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "artistNickname",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "artistNickname",
                table: "AspNetUsers");
        }
    }
}
