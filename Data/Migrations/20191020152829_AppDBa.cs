﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtSkills.Data.Migrations
{
    public partial class AppDBa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "AspNetUsers");
        }
    }
}
