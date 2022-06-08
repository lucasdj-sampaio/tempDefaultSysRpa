using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    public partial class addColumnsAtCharge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UraPhone",
                table: "TB_DataCharge",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WppNumber",
                table: "TB_DataCharge",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UraPhone",
                table: "TB_DataCharge");

            migrationBuilder.DropColumn(
                name: "WppNumber",
                table: "TB_DataCharge");
        }
    }
}
