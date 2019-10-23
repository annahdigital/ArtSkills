using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtSkills.Data.Migrations
{
    public partial class AppDBBb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArtId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Arts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentText = table.Column<string>(nullable: true),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PostedArtId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Arts_PostedArtId",
                        column: x => x.PostedArtId,
                        principalTable: "Arts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    taskListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TaskLists_taskListId",
                        column: x => x.taskListId,
                        principalTable: "TaskLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_ApplicationUserId",
                table: "TaskLists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ArtId",
                table: "AspNetUsers",
                column: "ArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Arts_UserId",
                table: "Arts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostedArtId",
                table: "Comment",
                column: "PostedArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_taskListId",
                table: "Task",
                column: "taskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arts_AspNetUsers_UserId",
                table: "Arts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Arts_ArtId",
                table: "AspNetUsers",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_AspNetUsers_ApplicationUserId",
                table: "TaskLists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arts_AspNetUsers_UserId",
                table: "Arts");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Arts_ArtId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_AspNetUsers_ApplicationUserId",
                table: "TaskLists");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_ApplicationUserId",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ArtId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Arts_UserId",
                table: "Arts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "ArtId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Arts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
