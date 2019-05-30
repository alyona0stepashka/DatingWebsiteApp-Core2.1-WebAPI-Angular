using Microsoft.EntityFrameworkCore.Migrations;

namespace App.DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FileModels_FileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sexes_SexId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalTypes_TypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Educations_EducationId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_FamilyStatuses_FamilyStatusId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_FinanceStatuses_FinanceStatusId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Nationalities_NationalityId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Zodiacs_ZodiacId",
                table: "PersonalTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ZodiacId",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Growth",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "FinanceStatusId",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FamilyStatusId",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "PersonalTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SexId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FileModels_FileId",
                table: "AspNetUsers",
                column: "FileId",
                principalTable: "FileModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sexes_SexId",
                table: "AspNetUsers",
                column: "SexId",
                principalTable: "Sexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalTypes_TypeId",
                table: "AspNetUsers",
                column: "TypeId",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Educations_EducationId",
                table: "PersonalTypes",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_FamilyStatuses_FamilyStatusId",
                table: "PersonalTypes",
                column: "FamilyStatusId",
                principalTable: "FamilyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_FinanceStatuses_FinanceStatusId",
                table: "PersonalTypes",
                column: "FinanceStatusId",
                principalTable: "FinanceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Nationalities_NationalityId",
                table: "PersonalTypes",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Zodiacs_ZodiacId",
                table: "PersonalTypes",
                column: "ZodiacId",
                principalTable: "Zodiacs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FileModels_FileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sexes_SexId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalTypes_TypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Educations_EducationId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_FamilyStatuses_FamilyStatusId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_FinanceStatuses_FinanceStatusId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Nationalities_NationalityId",
                table: "PersonalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTypes_Zodiacs_ZodiacId",
                table: "PersonalTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ZodiacId",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Growth",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FinanceStatusId",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FamilyStatusId",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "PersonalTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SexId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FileModels_FileId",
                table: "AspNetUsers",
                column: "FileId",
                principalTable: "FileModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sexes_SexId",
                table: "AspNetUsers",
                column: "SexId",
                principalTable: "Sexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalTypes_TypeId",
                table: "AspNetUsers",
                column: "TypeId",
                principalTable: "PersonalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Educations_EducationId",
                table: "PersonalTypes",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_FamilyStatuses_FamilyStatusId",
                table: "PersonalTypes",
                column: "FamilyStatusId",
                principalTable: "FamilyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_FinanceStatuses_FinanceStatusId",
                table: "PersonalTypes",
                column: "FinanceStatusId",
                principalTable: "FinanceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Nationalities_NationalityId",
                table: "PersonalTypes",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTypes_Zodiacs_ZodiacId",
                table: "PersonalTypes",
                column: "ZodiacId",
                principalTable: "Zodiacs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
