using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class cascade_deleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_PhotoAlbums_PhotoAlbumId",
                table: "FileModels");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_PhotoAlbums_PhotoAlbumId",
                table: "FileModels",
                column: "PhotoAlbumId",
                principalTable: "PhotoAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_PhotoAlbums_PhotoAlbumId",
                table: "FileModels");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_PhotoAlbums_PhotoAlbumId",
                table: "FileModels",
                column: "PhotoAlbumId",
                principalTable: "PhotoAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
