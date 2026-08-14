using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userassigmentAddedtoALD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Activities",
                newName: "AssignedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                newName: "IX_Activities_AssignedUserId");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_AssignedUserId",
                table: "Leads",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedByUserId",
                table: "Activities",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_AssignedUserId",
                table: "Activities",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedByUserId",
                table: "Activities",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

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
                name: "FK_Activities_Users_AssignedUserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedByUserId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_AssignedUserId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_CreatedByUserId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_AssignedUserId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CreatedByUserId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Leads",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_CreatedByUserId",
                table: "Leads",
                newName: "IX_Leads_AssignedToId");

            migrationBuilder.RenameColumn(
                name: "AssignedUserId",
                table: "Activities",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_AssignedUserId",
                table: "Activities",
                newName: "IX_Activities_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                table: "Activities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
