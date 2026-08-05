using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentWebPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStudentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudenName",
                table: "Students",
                newName: "StudentName");

            migrationBuilder.RenameColumn(
                name: "Attendence",
                table: "Students",
                newName: "Attendance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentName",
                table: "Students",
                newName: "StudenName");

            migrationBuilder.RenameColumn(
                name: "Attendance",
                table: "Students",
                newName: "Attendence");
        }
    }
}
