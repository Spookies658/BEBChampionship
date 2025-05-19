using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEBChampionship.Migrations
{
    /// <inheritdoc />
    public partial class winsandmajors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageFIR",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "AverageGIR",
                table: "Leaderboards");

            migrationBuilder.AddColumn<int>(
                name: "MajorWins",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MajorWins",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Leaderboards");

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
        }
    }
}
