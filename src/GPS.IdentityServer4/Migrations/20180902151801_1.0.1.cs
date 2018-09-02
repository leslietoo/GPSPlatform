using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPS.IdentityServer4.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimsJson",
                table: "GPS_Token",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GPS_RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ClientIp = table.Column<string>(nullable: true),
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
                name: "GPS_Token_History",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    ClientIp = table.Column<string>(nullable: true),
                    ClaimsJson = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    ResultCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPS_Token_History", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GPS_RefreshToken");

            migrationBuilder.DropTable(
                name: "GPS_Token_History");

            migrationBuilder.DropColumn(
                name: "ClaimsJson",
                table: "GPS_Token");
        }
    }
}
