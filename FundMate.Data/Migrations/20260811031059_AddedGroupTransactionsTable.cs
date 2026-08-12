using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FundMate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedGroupTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaidById = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    RecipientGroupId = table.Column<int>(type: "integer", nullable: false),
                    CreadtedById = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTransactions_Groups_RecipientGroupId",
                        column: x => x.RecipientGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTransactions_Users_CreadtedById",
                        column: x => x.CreadtedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTransactions_Users_PaidById",
                        column: x => x.PaidById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTransactions_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_CreadtedById",
                table: "GroupTransactions",
                column: "CreadtedById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_PaidById",
                table: "GroupTransactions",
                column: "PaidById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_RecipientGroupId",
                table: "GroupTransactions",
                column: "RecipientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransactions_UpdatedById",
                table: "GroupTransactions",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTransactions");
        }
    }
}
