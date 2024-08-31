using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixedspellingerrorinsurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfServey",
                table: "Surveys",
                newName: "YearOfSurvey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfSurvey",
                table: "Surveys",
                newName: "YearOfServey");
        }
    }
}
