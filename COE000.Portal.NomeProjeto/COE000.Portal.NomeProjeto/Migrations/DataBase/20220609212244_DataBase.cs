using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    public partial class DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FunctionTypeModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionTypeModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Environment",
                columns: table => new
                {
                    EnvCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnvironmentName = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Environment", x => x.EnvCode);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserConfig",
                columns: table => new
                {
                    ConfigCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserConfig", x => x.ConfigCode);
                });

            migrationBuilder.CreateTable(
                name: "TB_Legacy",
                columns: table => new
                {
                    LegacyCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    DateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Observation = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: true),
                    UserCode = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    FunctionCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Legacy", x => x.LegacyCode);
                    table.ForeignKey(
                        name: "FK_TB_Legacy_AspNetUsers_UserCode",
                        column: x => x.UserCode,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Legacy_FunctionTypeModel_FunctionCode",
                        column: x => x.FunctionCode,
                        principalTable: "FunctionTypeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_RpaCredential",
                columns: table => new
                {
                    CredentionCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    UserName = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    EnvCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RpaCredential", x => x.CredentionCode);
                    table.ForeignKey(
                        name: "FK_TB_RpaCredential_TB_Environment_EnvCode",
                        column: x => x.EnvCode,
                        principalTable: "TB_Environment",
                        principalColumn: "EnvCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Legacy_FunctionCode",
                table: "TB_Legacy",
                column: "FunctionCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Legacy_UserCode",
                table: "TB_Legacy",
                column: "UserCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential",
                column: "EnvCode");

            migrationBuilder.Sql(@"ALTER TABLE TB_LEGACY ADD 
            FOREIGN KEY (UserCode) REFERENCES AspNetUsers(Id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Legacy");

            migrationBuilder.DropTable(
                name: "TB_RpaCredential");

            migrationBuilder.DropTable(
                name: "TB_UserConfig");

            migrationBuilder.DropTable(
                name: "FunctionTypeModel");

            migrationBuilder.DropTable(
                name: "TB_Environment");
        }
    }
}
