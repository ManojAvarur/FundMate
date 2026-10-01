using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedAduitableFieldsTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreadtedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_CreadtedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_CreadtedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_CreadtedById",
                table: "Relations");

            migrationBuilder.RenameColumn(
                name: "CreadtedById",
                table: "Relations",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Relations_CreadtedById",
                table: "Relations",
                newName: "IX_Relations_CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreadtedById",
                table: "GroupUsers",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_CreadtedById",
                table: "GroupUsers",
                newName: "IX_GroupUsers_CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreadtedById",
                table: "GroupTransactions",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTransactions_CreadtedById",
                table: "GroupTransactions",
                newName: "IX_GroupTransactions_CreatedById");

            migrationBuilder.RenameColumn(
                name: "CreadtedById",
                table: "Groups",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreadtedById",
                table: "Groups",
                newName: "IX_Groups_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Users_CreatedById",
                table: "GroupTransactions",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Users_CreatedById",
                table: "GroupUsers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_CreatedById",
                table: "Relations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_CreatedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_CreatedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_CreatedById",
                table: "Relations");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Relations",
                newName: "CreadtedById");

            migrationBuilder.RenameIndex(
                name: "IX_Relations_CreatedById",
                table: "Relations",
                newName: "IX_Relations_CreadtedById");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "GroupUsers",
                newName: "CreadtedById");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_CreatedById",
                table: "GroupUsers",
                newName: "IX_GroupUsers_CreadtedById");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "GroupTransactions",
                newName: "CreadtedById");

            migrationBuilder.RenameIndex(
                name: "IX_GroupTransactions_CreatedById",
                table: "GroupTransactions",
                newName: "IX_GroupTransactions_CreadtedById");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Groups",
                newName: "CreadtedById");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatedById",
                table: "Groups",
                newName: "IX_Groups_CreadtedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreadtedById",
                table: "Groups",
                column: "CreadtedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Users_CreadtedById",
                table: "GroupTransactions",
                column: "CreadtedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Users_CreadtedById",
                table: "GroupUsers",
                column: "CreadtedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_CreadtedById",
                table: "Relations",
                column: "CreadtedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
