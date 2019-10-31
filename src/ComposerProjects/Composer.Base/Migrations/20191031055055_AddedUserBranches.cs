using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Composer.Base.Migrations
{
    public partial class AddedUserBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBranch",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBranch", x => new { x.BranchId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBranch_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ProjectRole",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1L, "Admin", "Администратор" });

            migrationBuilder.InsertData(
                table: "ProjectRole",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2L, "Manager", "Менеджер" });

            migrationBuilder.InsertData(
                table: "ProjectRole",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 3L, "Engineer", "Инженер" });

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_UserId",
                table: "UserBranch",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBranch");

            migrationBuilder.DeleteData(
                table: "ProjectRole",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProjectRole",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProjectRole",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
