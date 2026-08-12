using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLookupToUsersFromRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Relations_UserIdOne",
                table: "Relations",
                column: "UserIdOne");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_UserIdTwo",
                table: "Relations",
                column: "UserIdTwo");

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_UserIdOne",
                table: "Relations",
                column: "UserIdOne",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_UserIdTwo",
                table: "Relations",
                column: "UserIdTwo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_UserIdOne",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_UserIdTwo",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_UserIdOne",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_UserIdTwo",
                table: "Relations");
        }
    }
}
