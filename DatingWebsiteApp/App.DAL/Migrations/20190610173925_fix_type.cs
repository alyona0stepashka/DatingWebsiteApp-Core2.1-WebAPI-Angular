using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class fix_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadHabitUsers_PersonalTypes_PersonalTypeId1",
                table: "BadHabitUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestUsers_PersonalTypes_PersonalTypeId1",
                table: "InterestUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageUsers_PersonalTypes_PersonalTypeId1",
                table: "LanguageUsers");

            migrationBuilder.DropIndex(
                name: "IX_LanguageUsers_PersonalTypeId1",
                table: "LanguageUsers");

            migrationBuilder.DropIndex(
                name: "IX_InterestUsers_PersonalTypeId1",
                table: "InterestUsers");

            migrationBuilder.DropIndex(
                name: "IX_BadHabitUsers_PersonalTypeId1",
                table: "BadHabitUsers");

            migrationBuilder.DropColumn(
                name: "PersonalTypeId1",
                table: "LanguageUsers");

            migrationBuilder.DropColumn(
                name: "PersonalTypeId1",
                table: "InterestUsers");

            migrationBuilder.DropColumn(
                name: "PersonalTypeId1",
                table: "BadHabitUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalTypeId",
                table: "LanguageUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonalTypeId",
                table: "InterestUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonalTypeId",
                table: "BadHabitUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageUsers_PersonalTypeId",
                table: "LanguageUsers",
                column: "PersonalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestUsers_PersonalTypeId",
                table: "InterestUsers",
                column: "PersonalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BadHabitUsers_PersonalTypeId",
                table: "BadHabitUsers",
                column: "PersonalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BadHabitUsers_PersonalTypes_PersonalTypeId",
                table: "BadHabitUsers",
                column: "PersonalTypeId",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestUsers_PersonalTypes_PersonalTypeId",
                table: "InterestUsers",
                column: "PersonalTypeId",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageUsers_PersonalTypes_PersonalTypeId",
                table: "LanguageUsers",
                column: "PersonalTypeId",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadHabitUsers_PersonalTypes_PersonalTypeId",
                table: "BadHabitUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestUsers_PersonalTypes_PersonalTypeId",
                table: "InterestUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageUsers_PersonalTypes_PersonalTypeId",
                table: "LanguageUsers");

            migrationBuilder.DropIndex(
                name: "IX_LanguageUsers_PersonalTypeId",
                table: "LanguageUsers");

            migrationBuilder.DropIndex(
                name: "IX_InterestUsers_PersonalTypeId",
                table: "InterestUsers");

            migrationBuilder.DropIndex(
                name: "IX_BadHabitUsers_PersonalTypeId",
                table: "BadHabitUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalTypeId",
                table: "LanguageUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PersonalTypeId1",
                table: "LanguageUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalTypeId",
                table: "InterestUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PersonalTypeId1",
                table: "InterestUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalTypeId",
                table: "BadHabitUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PersonalTypeId1",
                table: "BadHabitUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageUsers_PersonalTypeId1",
                table: "LanguageUsers",
                column: "PersonalTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_InterestUsers_PersonalTypeId1",
                table: "InterestUsers",
                column: "PersonalTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_BadHabitUsers_PersonalTypeId1",
                table: "BadHabitUsers",
                column: "PersonalTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BadHabitUsers_PersonalTypes_PersonalTypeId1",
                table: "BadHabitUsers",
                column: "PersonalTypeId1",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestUsers_PersonalTypes_PersonalTypeId1",
                table: "InterestUsers",
                column: "PersonalTypeId1",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageUsers_PersonalTypes_PersonalTypeId1",
                table: "LanguageUsers",
                column: "PersonalTypeId1",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
