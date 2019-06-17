using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class chat_begin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Chats",
                newName: "IsBlock");

            migrationBuilder.RenameColumn(
                name: "IsReaded",
                table: "ChatMessages",
                newName: "IsNew");

            migrationBuilder.AddColumn<DateTime>(
                name: "UserFromClearHistory",
                table: "Chats",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UserToClearHistory",
                table: "Chats",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFromClearHistory",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserToClearHistory",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "IsBlock",
                table: "Chats",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "ChatMessages",
                newName: "IsReaded");
        }
    }
}
