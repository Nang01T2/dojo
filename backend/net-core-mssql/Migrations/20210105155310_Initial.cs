using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace net_core_mssql.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Player")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HitPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false),
                    Intelligence = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkills",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkills", x => new { x.CharacterId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 1, 30, "Fireball" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 2, 20, "Frenzy" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 3, 50, "Blizzard" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new byte[] { 82, 107, 154, 99, 233, 89, 155, 156, 185, 75, 39, 25, 109, 226, 157, 226, 81, 71, 99, 221, 247, 199, 87, 238, 12, 197, 32, 61, 173, 128, 199, 250, 195, 213, 5, 38, 16, 17, 184, 204, 87, 53, 41, 248, 126, 193, 193, 138, 249, 82, 184, 11, 98, 61, 22, 241, 75, 49, 170, 127, 185, 109, 135, 225 }, new byte[] { 240, 8, 119, 103, 218, 31, 239, 179, 66, 4, 123, 93, 6, 24, 69, 86, 170, 31, 246, 250, 225, 42, 15, 13, 101, 238, 180, 231, 161, 43, 237, 72, 3, 212, 142, 140, 211, 87, 181, 21, 163, 230, 6, 107, 154, 174, 151, 78, 162, 87, 91, 174, 205, 126, 11, 201, 121, 124, 30, 111, 208, 7, 222, 45, 51, 66, 19, 170, 111, 11, 93, 218, 204, 232, 85, 232, 218, 92, 188, 136, 152, 53, 255, 73, 200, 53, 126, 55, 189, 237, 53, 64, 247, 124, 168, 97, 170, 220, 187, 87, 201, 198, 104, 119, 136, 182, 98, 187, 71, 208, 40, 197, 50, 31, 119, 136, 208, 200, 75, 77, 47, 106, 174, 144, 172, 65, 111, 241 }, "User1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 2, new byte[] { 82, 107, 154, 99, 233, 89, 155, 156, 185, 75, 39, 25, 109, 226, 157, 226, 81, 71, 99, 221, 247, 199, 87, 238, 12, 197, 32, 61, 173, 128, 199, 250, 195, 213, 5, 38, 16, 17, 184, 204, 87, 53, 41, 248, 126, 193, 193, 138, 249, 82, 184, 11, 98, 61, 22, 241, 75, 49, 170, 127, 185, 109, 135, 225 }, new byte[] { 240, 8, 119, 103, 218, 31, 239, 179, 66, 4, 123, 93, 6, 24, 69, 86, 170, 31, 246, 250, 225, 42, 15, 13, 101, 238, 180, 231, 161, 43, 237, 72, 3, 212, 142, 140, 211, 87, 181, 21, 163, 230, 6, 107, 154, 174, 151, 78, 162, 87, 91, 174, 205, 126, 11, 201, 121, 124, 30, 111, 208, 7, 222, 45, 51, 66, 19, 170, 111, 11, 93, 218, 204, 232, 85, 232, 218, 92, 188, 136, 152, 53, 255, 73, 200, 53, 126, 55, 189, 237, 53, 64, 247, 124, 168, 97, 170, 220, 187, 87, 201, 198, 104, 119, 136, 182, 98, 187, 71, 208, 40, 197, 50, 31, 119, 136, 208, 200, 75, 77, 47, 106, 174, 144, 172, 65, 111, 241 }, "User2" });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_SkillId",
                table: "CharacterSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_CharacterId",
                table: "Weapons",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkills");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
