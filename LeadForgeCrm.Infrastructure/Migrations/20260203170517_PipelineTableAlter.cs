using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PipelineTableAlter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "PipelineStages");

            migrationBuilder.DropColumn(
                name: "IsWon",
                table: "PipelineStages");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PipelineStages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PipelineStages");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "PipelineStages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWon",
                table: "PipelineStages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
