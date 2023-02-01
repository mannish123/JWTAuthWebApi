using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTAuthWebApi.Migrations
{
    public partial class CreateSchoolDB5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Grades",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Grades",
                newName: "GradeId");
        }
    }
}
