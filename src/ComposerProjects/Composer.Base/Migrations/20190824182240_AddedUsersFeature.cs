using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Composer.Base.Migrations
{
    public partial class AddedUsersFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutocompleteKind",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutocompleteKind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    RegistredEmail = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LegalName = table.Column<string>(nullable: false),
                    Trademark = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    UserLogin = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:IdentityIncrement", 1)
                        .Annotation("SqlServer:IdentitySeed", 1)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutocompleteItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    AutocompleteKindId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutocompleteItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutocompleteItem_AutocompleteKind_AutocompleteKindId",
                        column: x => x.AutocompleteKindId,
                        principalTable: "AutocompleteKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutocompleteItem_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    ProjectRoleId = table.Column<long>(nullable: false),
                    LoginName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_ProjectRole_ProjectRoleId",
                        column: x => x.ProjectRoleId,
                        principalTable: "ProjectRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AutocompleteKind",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1L, "DeviceTrademark", "Бренд" });

            migrationBuilder.InsertData(
                table: "AutocompleteKind",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2L, "DeviceOptions", "Комплектация" });

            migrationBuilder.InsertData(
                table: "AutocompleteKind",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 3L, "DeviceAppearance", "Внешний вид" });

            migrationBuilder.CreateIndex(
                name: "IX_AutocompleteItem_AutocompleteKindId",
                table: "AutocompleteItem",
                column: "AutocompleteKindId");

            migrationBuilder.CreateIndex(
                name: "IX_AutocompleteItem_TenantId",
                table: "AutocompleteItem",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AutocompleteKind_Code",
                table: "AutocompleteKind",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRole_Code",
                table: "ProjectRole",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_RegistredEmail",
                table: "Tenant",
                column: "RegistredEmail",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_User_LoginName",
                table: "User",
                column: "LoginName",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProjectRoleId",
                table: "User",
                column: "ProjectRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutocompleteItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AutocompleteKind");

            migrationBuilder.DropTable(
                name: "ProjectRole");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
