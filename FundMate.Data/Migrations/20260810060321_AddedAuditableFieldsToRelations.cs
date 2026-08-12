using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditableFieldsToRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreadtedById",
                table: "Relations",
                type: "uuid",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Relations",
                type: "timestamp with time zone",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Relations",
                type: "timestamp with time zone",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Relations",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Relations_CreadtedById",
                table: "Relations",
                column: "CreadtedById");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_UpdatedById",
                table: "Relations",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_CreadtedById",
                table: "Relations",
                column: "CreadtedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_CreadtedById",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_CreadtedById",
                table: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_Relations_UpdatedById",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "CreadtedById",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Relations");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Relations");
        }
    }
}
