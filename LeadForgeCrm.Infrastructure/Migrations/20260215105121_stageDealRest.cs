using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class stageDealRest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PipelineStages_PipelineStageId",
                table: "Deals");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_PipelineId",
                table: "Deals",
                column: "PipelineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PipeLines_PipelineId",
                table: "Deals",
                column: "PipelineId",
                principalTable: "PipeLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PipelineStages_PipelineStageId",
                table: "Deals",
                column: "PipelineStageId",
                principalTable: "PipelineStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PipeLines_PipelineId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PipelineStages_PipelineStageId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_PipelineId",
                table: "Deals");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PipelineStages_PipelineStageId",
                table: "Deals",
                column: "PipelineStageId",
                principalTable: "PipelineStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
