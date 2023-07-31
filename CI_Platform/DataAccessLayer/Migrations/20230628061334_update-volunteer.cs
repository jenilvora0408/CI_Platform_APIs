using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatevolunteer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "availability",
                table: "volunteers",
                type: "int",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<string>(
                name: "reason_to_be_volunteer",
                table: "volunteers",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "volunteer_skills",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    volunteer_id = table.Column<long>(type: "bigint", nullable: false),
                    skill_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteer_skills", x => x.id);
                    table.ForeignKey(
                        name: "FK_volunteer_skills_skills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_volunteer_skills_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "banners",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "avatar", "modified_on" },
                values: new object[] { "/assets/avatar/profile.png", new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_volunteer_skills_skill_id",
                table: "volunteer_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_volunteer_skills_volunteer_id",
                table: "volunteer_skills",
                column: "volunteer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "volunteer_skills");

            migrationBuilder.DropColumn(
                name: "availability",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "reason_to_be_volunteer",
                table: "volunteers");

            migrationBuilder.UpdateData(
                table: "banners",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4154), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4155), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "avatar", "modified_on" },
                values: new object[] { "/wwwroot/assets/avatar/profile.png", new DateTimeOffset(new DateTime(2023, 6, 26, 13, 2, 54, 32, DateTimeKind.Unspecified).AddTicks(4009), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
