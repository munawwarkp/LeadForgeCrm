using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedAssignedUsertoDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_AssignedUserId",
                table: "Deals",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CreatedByUserId",
                table: "Deals",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_AssignedUserId",
                table: "Deals",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Users_CreatedByUserId",
                table: "Deals",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_AssignedUserId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Users_CreatedByUserId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_AssignedUserId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_CreatedByUserId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Deals");
        }
    }
}
