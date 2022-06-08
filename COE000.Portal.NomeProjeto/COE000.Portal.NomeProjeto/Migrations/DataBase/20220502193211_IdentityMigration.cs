using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COE000.Portal.NomeProjeto.Migrations.DataBase
{
    public partial class IdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TB_FunctionType",
                columns: table => new
                {
                    FunctionCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionName = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FunctionType", x => x.FunctionCode);
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

            migrationBuilder.CreateTable(
                name: "TB_Historic",
                columns: table => new
                {
                    HistoricCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    DateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserCode = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    FunctionCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Historic", x => x.HistoricCode);
                    table.ForeignKey(
                        name: "FK_TB_Historic_AspNetUsers_UserCode",
                        column: x => x.UserCode,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Historic_TB_FunctionType_FunctionCode",
                        column: x => x.FunctionCode,
                        principalTable: "TB_FunctionType",
                        principalColumn: "FunctionCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_DataCharge",
                columns: table => new
                {
                    ChargeId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    DateOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ClientDocument = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    EC = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    HistoricCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DataCharge", x => x.ChargeId);
                    table.ForeignKey(
                        name: "FK_TB_DataCharge_TB_Historic_HistoricCode",
                        column: x => x.HistoricCode,
                        principalTable: "TB_Historic",
                        principalColumn: "HistoricCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Status",
                columns: table => new
                {
                    StatusCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    FileName = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    DateOn = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    Observation = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: true),
                    HistoricCode = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    EnvCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Status", x => x.StatusCode);
                    table.ForeignKey(
                        name: "FK_TB_Status_TB_Environment_EnvCode",
                        column: x => x.EnvCode,
                        principalTable: "TB_Environment",
                        principalColumn: "EnvCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Status_TB_Historic_HistoricCode",
                        column: x => x.HistoricCode,
                        principalTable: "TB_Historic",
                        principalColumn: "HistoricCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_DataCharge_HistoricCode",
                table: "TB_DataCharge",
                column: "HistoricCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Historic_FunctionCode",
                table: "TB_Historic",
                column: "FunctionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Historic_UserCode",
                table: "TB_Historic",
                column: "UserCode");

            migrationBuilder.CreateIndex(
                name: "IX_TB_RpaCredential_EnvCode",
                table: "TB_RpaCredential",
                column: "EnvCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_EnvCode",
                table: "TB_Status",
                column: "EnvCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Status_HistoricCode",
                table: "TB_Status",
                column: "HistoricCode");

            migrationBuilder.Sql(@"ALTER TABLE TB_HISTORIC ADD 
                FOREIGN KEY (UserCode) REFERENCES AspNetUsers(Id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_DataCharge");

            migrationBuilder.DropTable(
                name: "TB_RpaCredential");

            migrationBuilder.DropTable(
                name: "TB_Status");

            migrationBuilder.DropTable(
                name: "TB_Environment");

            migrationBuilder.DropTable(
                name: "TB_Historic");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TB_FunctionType");
        }
    }
}
