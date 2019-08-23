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
                name: "ProjectRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DateModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    ProjectRoleID = table.Column<long>(nullable: false),
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
                        name: "FK_User_ProjectRole_ProjectRoleID",
                        column: x => x.ProjectRoleID,
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
                filter: "IsDeleted Is NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "IsDeleted Is NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_LoginName",
                table: "User",
                column: "LoginName",
                unique: true,
                filter: "IsDeleted Is NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ProjectRoleID",
                table: "User",
                column: "ProjectRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ProjectRole");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
