using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Composer.Base.Migrations
{
    public partial class AddedOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatusKind",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    OrderStatusKindId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatus_OrderStatusKind_OrderStatusKindId",
                        column: x => x.OrderStatusKindId,
                        principalTable: "OrderStatusKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatus_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderStatusKind",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1L, "New", "Новые" },
                    { 2L, "OnWork", "На исполнении" },
                    { 3L, "Suspended", "Отложенные" },
                    { 4L, "Completed", "Исполненные" },
                    { 5L, "Closed", "Закрытые" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_OrderStatusKindId",
                table: "OrderStatus",
                column: "OrderStatusKindId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_TenantId",
                table: "OrderStatus",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusKind_Code",
                table: "OrderStatusKind",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "OrderStatusKind");
        }
    }
}
