using Microsoft.EntityFrameworkCore.Migrations;

namespace JWTAuthWebApi.Migrations
{
    public partial class CreateSchoolDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Grades",
                newName: "GradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Grades",
                newName: "Id");
        }
    }
}
