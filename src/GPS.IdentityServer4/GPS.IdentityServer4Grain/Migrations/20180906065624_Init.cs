using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPS.IdentityServer4Grain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPS_RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ClaimsJson = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    OldClaimsJson = table.Column<string>(nullable: true),
                    OldToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPS_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPS_Token",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ClaimsJson = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPS_Token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPS_VerifyToken",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ClaimsJson = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    ResultCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPS_VerifyToken", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GPS_RefreshToken");

            migrationBuilder.DropTable(
                name: "GPS_Token");

            migrationBuilder.DropTable(
                name: "GPS_VerifyToken");
        }
    }
}
