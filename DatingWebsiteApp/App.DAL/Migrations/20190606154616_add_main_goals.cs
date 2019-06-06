using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class add_main_goals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainGoal",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MainGoalId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainGoals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainGoals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MainGoalId",
                table: "AspNetUsers",
                column: "MainGoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MainGoals_MainGoalId",
                table: "AspNetUsers",
                column: "MainGoalId",
                principalTable: "MainGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MainGoals_MainGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MainGoals");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MainGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MainGoalId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MainGoal",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
