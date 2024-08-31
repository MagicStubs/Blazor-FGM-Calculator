using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearOfServey = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FifteenToNineteen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TwentyToTwentyfour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TwentyToTwentynine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThirtyToThitryfour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThirtyfiveToThitrynine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FourtyToFourtyfour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FourtyfiveToFourtynine = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => new { x.Name, x.YearOfServey, x.CountryName, x.CountryCode });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
