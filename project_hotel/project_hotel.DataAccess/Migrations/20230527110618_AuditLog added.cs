using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_hotel.DataAccess.Migrations
{
    public partial class AuditLogadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UseCase = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAuthorized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UseCase",
                table: "AuditLog",
                column: "UseCase");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_Username",
                table: "AuditLog",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");
        }
    }
}
