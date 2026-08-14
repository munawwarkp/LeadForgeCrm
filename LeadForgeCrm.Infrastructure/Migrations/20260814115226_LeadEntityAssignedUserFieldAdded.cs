using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeadEntityAssignedUserFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_AssignedToId",
                table: "Leads");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "Leads",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_AssignedToId",
                table: "Leads",
                newName: "IX_Leads_CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedUserId",
                table: "Leads",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Users_AssignedUserId",
                table: "Leads",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Users_CreatedByUserId",
                table: "Leads",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_AssignedUserId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_CreatedByUserId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_AssignedUserId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Leads");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Leads",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_CreatedByUserId",
                table: "Leads",
                newName: "IX_Leads_AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Users_AssignedToId",
                table: "Leads",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
