using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEBChampionship.Migrations
{
    /// <inheritdoc />
    public partial class updatestats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageFIR",
                table: "Leaderboards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageGIR",
                table: "Leaderboards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPutts",
                table: "Leaderboards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageFIR",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "AverageGIR",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "TotalPutts",
                table: "Leaderboards");
        }
    }
}
