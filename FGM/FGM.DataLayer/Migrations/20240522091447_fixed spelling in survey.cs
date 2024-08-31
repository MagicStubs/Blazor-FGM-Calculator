using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class fixedspellinginsurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwentyFiveToTwentynine",
                table: "Surveys",
                newName: "TwentyfiveToTwentynine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwentyfiveToTwentynine",
                table: "Surveys",
                newName: "TwentyFiveToTwentynine");
        }
    }
}
