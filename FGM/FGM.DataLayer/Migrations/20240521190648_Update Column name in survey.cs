using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnnameinsurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwentyToTwentynine",
                table: "Surveys",
                newName: "TwentyFiveToTwentynine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TwentyFiveToTwentynine",
                table: "Surveys",
                newName: "TwentyToTwentynine");
        }
    }
}
