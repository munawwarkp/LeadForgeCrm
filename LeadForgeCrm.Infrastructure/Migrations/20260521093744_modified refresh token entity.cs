using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadForgeCrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifiedrefreshtokenentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Contacts_ContactId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Deals_DealId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Leads_LeadId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshTokens",
                newName: "TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_Address_LeadId",
                table: "Addresses",
                newName: "IX_Addresses_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_DealId",
                table: "Addresses",
                newName: "IX_Addresses_DealId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ContactId",
                table: "Addresses",
                newName: "IX_Addresses_ContactId");

            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Contacts_ContactId",
                table: "Addresses",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Deals_DealId",
                table: "Addresses",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Leads_LeadId",
                table: "Addresses",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Contacts_ContactId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Deals_DealId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Leads_LeadId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "RefreshTokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_LeadId",
                table: "Address",
                newName: "IX_Address_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_DealId",
                table: "Address",
                newName: "IX_Address_DealId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ContactId",
                table: "Address",
                newName: "IX_Address_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Contacts_ContactId",
                table: "Address",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Deals_DealId",
                table: "Address",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Leads_LeadId",
                table: "Address",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id");
        }
    }
}
