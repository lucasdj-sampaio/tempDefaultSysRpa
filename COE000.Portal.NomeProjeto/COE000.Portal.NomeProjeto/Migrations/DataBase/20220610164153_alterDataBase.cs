using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    public partial class alterDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOn",
                table: "TB_UserConfig",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOn",
                table: "TB_UserConfig");
        }
    }
}
