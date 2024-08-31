using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class morespellingfixs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThirtyToThitryfour",
                table: "Surveys",
                newName: "ThirtyToThirtyfour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThirtyToThirtyfour",
                table: "Surveys",
                newName: "ThirtyToThitryfour");
        }
    }
}
