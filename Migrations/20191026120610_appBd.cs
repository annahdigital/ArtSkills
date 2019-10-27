using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtSkills.Migrations
{
    public partial class appBd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Arts_ArtId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowArtist_AspNetUsers_ArtistId",
                table: "FollowArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowArtist_AspNetUsers_FollowerId",
                table: "FollowArtist");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_ApplicationUserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Arts_ArtId",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowArtist",
                table: "FollowArtist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "FollowArtist",
                newName: "FollowArtists");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ArtId",
                table: "Likes",
                newName: "IX_Likes_ArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ApplicationUserId",
                table: "Likes",
                newName: "IX_Likes_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowArtist_FollowerId",
                table: "FollowArtists",
                newName: "IX_FollowArtists_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowArtist_ArtistId",
                table: "FollowArtists",
                newName: "IX_FollowArtists_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ArtId",
                table: "Comments",
                newName: "IX_Comments_ArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ApplicationUserId",
                table: "Comments",
                newName: "IX_Comments_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowArtists",
                table: "FollowArtists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Arts_ArtId",
                table: "Comments",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowArtists_AspNetUsers_ArtistId",
                table: "FollowArtists",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowArtists_AspNetUsers_FollowerId",
                table: "FollowArtists",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId",
                table: "Likes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Arts_ArtId",
                table: "Likes",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Arts_ArtId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowArtists_AspNetUsers_ArtistId",
                table: "FollowArtists");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowArtists_AspNetUsers_FollowerId",
                table: "FollowArtists");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_ApplicationUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Arts_ArtId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowArtists",
                table: "FollowArtists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "FollowArtists",
                newName: "FollowArtist");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ArtId",
                table: "Like",
                newName: "IX_Like_ArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ApplicationUserId",
                table: "Like",
                newName: "IX_Like_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowArtists_FollowerId",
                table: "FollowArtist",
                newName: "IX_FollowArtist_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowArtists_ArtistId",
                table: "FollowArtist",
                newName: "IX_FollowArtist_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ArtId",
                table: "Comment",
                newName: "IX_Comment_ArtId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comment",
                newName: "IX_Comment_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowArtist",
                table: "FollowArtist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ApplicationUserId",
                table: "Comment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Arts_ArtId",
                table: "Comment",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowArtist_AspNetUsers_ArtistId",
                table: "FollowArtist",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowArtist_AspNetUsers_FollowerId",
                table: "FollowArtist",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_ApplicationUserId",
                table: "Like",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Arts_ArtId",
                table: "Like",
                column: "ArtId",
                principalTable: "Arts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
