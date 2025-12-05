using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecretSantaBackend.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Employees_GiverId",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Employees_ReceiverId",
                table: "Pairs");

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Pairs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SecretSantaLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UnpairedEmployeeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretSantaLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecretSantaLists_Employees_UnpairedEmployeeId",
                        column: x => x.UnpairedEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_ListId",
                table: "Pairs",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretSantaLists_UnpairedEmployeeId",
                table: "SecretSantaLists",
                column: "UnpairedEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Employees_GiverId",
                table: "Pairs",
                column: "GiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Employees_ReceiverId",
                table: "Pairs",
                column: "ReceiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_SecretSantaLists_ListId",
                table: "Pairs",
                column: "ListId",
                principalTable: "SecretSantaLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Employees_GiverId",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Employees_ReceiverId",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_SecretSantaLists_ListId",
                table: "Pairs");

            migrationBuilder.DropTable(
                name: "SecretSantaLists");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_ListId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Employees_GiverId",
                table: "Pairs",
                column: "GiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Employees_ReceiverId",
                table: "Pairs",
                column: "ReceiverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
