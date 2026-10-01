using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGroupTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Groups_RecipientGroupId",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_PaidById",
                table: "GroupTransactions");

            migrationBuilder.DropIndex(
                name: "IX_GroupTransactions_RecipientGroupId",
                table: "GroupTransactions");

            migrationBuilder.DropColumn(
                name: "RecipientGroupId",
                table: "GroupTransactions");

            migrationBuilder.Sql(@"
                ALTER TABLE ""GroupTransactions"" DROP COLUMN ""PaidById"";
        
                ALTER TABLE ""GroupTransactions"" ADD COLUMN ""PaidById"" INTEGER NOT null;
            ");

            //migrationBuilder.AlterColumn<int>(
            //    name: "PaidById",
            //    table: "GroupTransactions",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_GroupUsers_PaidById",
                table: "GroupTransactions",
                column: "PaidById",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_GroupUsers_PaidById",
                table: "GroupTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaidById",
                table: "GroupTransactions",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "RecipientGroupId",
                table: "GroupTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_RecipientGroupId",
                table: "GroupTransactions",
                column: "RecipientGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Groups_RecipientGroupId",
                table: "GroupTransactions",
                column: "RecipientGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Users_PaidById",
                table: "GroupTransactions",
                column: "PaidById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
