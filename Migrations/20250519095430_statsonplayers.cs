using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEBChampionship.Migrations
{
    /// <inheritdoc />
    public partial class statsonplayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageNetScore",
                table: "Leaderboards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BestNetScore",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorstNetScore",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageNetScore",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "BestNetScore",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "WorstNetScore",
                table: "Leaderboards");
        }
    }
}
