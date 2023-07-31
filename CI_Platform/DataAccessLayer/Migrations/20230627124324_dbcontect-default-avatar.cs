using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class dbcontectdefaultavatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValueSql: "'/assets/avatar/profile.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldDefaultValueSql: "'/wwwroot/assets/avatar/profile.png'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValueSql: "'/wwwroot/assets/avatar/profile.png'",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldDefaultValueSql: "'/assets/avatar/profile.png'");
        }
    }
}
