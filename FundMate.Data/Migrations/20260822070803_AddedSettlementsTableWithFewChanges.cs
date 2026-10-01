using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSettlementsTableWithFewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UpdatedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_CreatedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_UpdatedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_CreatedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_UpdatedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_CreatedById",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_SplitTypes_Users_CreatedById",
                table: "SplitTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SplitTypes_Users_UpdatedById",
                table: "SplitTypes");

            migrationBuilder.AddColumn<int>(
                name: "SplitTypeId",
                table: "GroupTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    PayerId = table.Column<int>(type: "integer", nullable: false),
                    PayeeId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.CheckConstraint("CK_Settlements_PayerPayeeDiffer", "\"PayerId\" <> \"PayeeId\"");
                    table.ForeignKey(
                        name: "FK_Settlements_GroupUsers_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settlements_GroupUsers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settlements_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Settlements_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Settlements_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupTransactionId = table.Column<int>(type: "integer", nullable: false),
                    GroupUserId = table.Column<int>(type: "integer", nullable: false),
                    SplitValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    OwedAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSubscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionSubscribers_GroupTransactions_GroupTransactionId",
                        column: x => x.GroupTransactionId,
                        principalTable: "GroupTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionSubscribers_GroupUsers_GroupUserId",
                        column: x => x.GroupUserId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionSubscribers_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionSubscribers_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_SplitTypeId",
                table: "GroupTransactions",
                column: "SplitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CreatedById",
                table: "Settlements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_GroupId",
                table: "Settlements",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_PayeeId",
                table: "Settlements",
                column: "PayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_PayerId",
                table: "Settlements",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_UpdatedById",
                table: "Settlements",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSubscribers_CreatedById",
                table: "TransactionSubscribers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSubscribers_GroupTransactionId",
                table: "TransactionSubscribers",
                column: "GroupTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSubscribers_GroupUserId",
                table: "TransactionSubscribers",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSubscribers_UpdatedById",
                table: "TransactionSubscribers",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UpdatedById",
                table: "Groups",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_SplitTypes_SplitTypeId",
                table: "GroupTransactions",
                column: "SplitTypeId",
                principalTable: "SplitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Users_CreatedById",
                table: "GroupTransactions",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTransactions_Users_UpdatedById",
                table: "GroupTransactions",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Users_CreatedById",
                table: "GroupUsers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Users_UpdatedById",
                table: "GroupUsers",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_CreatedById",
                table: "Relations",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SplitTypes_Users_CreatedById",
                table: "SplitTypes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SplitTypes_Users_UpdatedById",
                table: "SplitTypes",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_UpdatedById",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_SplitTypes_SplitTypeId",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_CreatedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTransactions_Users_UpdatedById",
                table: "GroupTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_CreatedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_UpdatedById",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_CreatedById",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_SplitTypes_Users_CreatedById",
                table: "SplitTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SplitTypes_Users_UpdatedById",
                table: "SplitTypes");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "TransactionSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_GroupTransactions_SplitTypeId",
                table: "GroupTransactions");

            migrationBuilder.DropColumn(
                name: "SplitTypeId",
                table: "GroupTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatedById",
                table: "Groups",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_UpdatedById",
                table: "Groups",
                column: "UpdatedById",
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
                name: "FK_GroupTransactions_Users_UpdatedById",
                table: "GroupTransactions",
                column: "UpdatedById",
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
                name: "FK_GroupUsers_Users_UpdatedById",
                table: "GroupUsers",
                column: "UpdatedById",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_Users_UpdatedById",
                table: "Relations",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SplitTypes_Users_CreatedById",
                table: "SplitTypes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SplitTypes_Users_UpdatedById",
                table: "SplitTypes",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
