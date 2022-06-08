using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    public partial class alterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_Status_EnvCode",
                table: "TB_Status");

            migrationBuilder.DropIndex(
                name: "IX_TB_Status_TypeCode",
                table: "TB_Status");

            migrationBuilder.DropIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential");

            migrationBuilder.DropIndex(
                name: "IX_TB_Historic_FunctionCode",
                table: "TB_Historic");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "TB_Status",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "TB_Historic",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOn",
                table: "TB_DataCharge",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "TB_DataCharge",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_EnvCode",
                table: "TB_Status",
                column: "EnvCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_TypeCode",
                table: "TB_Status",
                column: "TypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential",
                column: "EnvCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Historic_FunctionCode",
                table: "TB_Historic",
                column: "FunctionCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_Status_EnvCode",
                table: "TB_Status");

            migrationBuilder.DropIndex(
                name: "IX_TB_Status_TypeCode",
                table: "TB_Status");

            migrationBuilder.DropIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential");

            migrationBuilder.DropIndex(
                name: "IX_TB_Historic_FunctionCode",
                table: "TB_Historic");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "TB_Historic");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "TB_Status",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOn",
                table: "TB_DataCharge",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "TB_DataCharge",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_EnvCode",
                table: "TB_Status",
                column: "EnvCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_TypeCode",
                table: "TB_Status",
                column: "TypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential",
                column: "EnvCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Historic_FunctionCode",
                table: "TB_Historic",
                column: "FunctionCode",
                unique: true);
        }
    }
}
