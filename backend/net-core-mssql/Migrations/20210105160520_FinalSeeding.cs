using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace net_core_mssql.Migrations
{
    public partial class FinalSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defense", "HitPoints", "Intelligence", "Name", "Strength", "UserId" },
                values: new object[] { 1, 1, 10, 100, 10, "Frodo", 15, 1 });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Class", "Defense", "HitPoints", "Intelligence", "Name", "Strength", "UserId" },
                values: new object[] { 2, 2, 5, 100, 20, "Raistlin", 5, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 243, 61, 135, 86, 254, 239, 214, 154, 252, 56, 179, 4, 157, 111, 149, 65, 165, 125, 81, 9, 174, 8, 232, 125, 160, 248, 199, 70, 194, 83, 61, 161, 12, 243, 62, 219, 152, 155, 96, 92, 215, 69, 161, 45, 124, 36, 8, 18, 148, 211, 212, 147, 107, 184, 11, 64, 192, 199, 245, 35, 126, 5, 227, 29 }, new byte[] { 9, 41, 206, 44, 101, 147, 82, 15, 78, 85, 47, 88, 242, 29, 165, 5, 36, 135, 125, 198, 118, 194, 170, 188, 136, 232, 198, 188, 167, 97, 38, 49, 114, 60, 205, 85, 53, 5, 253, 86, 79, 94, 82, 40, 248, 95, 24, 158, 164, 38, 25, 166, 201, 242, 222, 84, 184, 172, 22, 100, 233, 33, 157, 165, 178, 37, 32, 221, 8, 216, 139, 64, 149, 200, 212, 115, 210, 152, 203, 180, 243, 28, 88, 145, 1, 118, 247, 224, 33, 174, 57, 7, 193, 129, 143, 146, 149, 227, 210, 212, 43, 186, 6, 240, 70, 2, 9, 114, 94, 108, 47, 246, 53, 14, 231, 246, 248, 130, 80, 61, 169, 73, 156, 230, 63, 245, 158, 76 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 243, 61, 135, 86, 254, 239, 214, 154, 252, 56, 179, 4, 157, 111, 149, 65, 165, 125, 81, 9, 174, 8, 232, 125, 160, 248, 199, 70, 194, 83, 61, 161, 12, 243, 62, 219, 152, 155, 96, 92, 215, 69, 161, 45, 124, 36, 8, 18, 148, 211, 212, 147, 107, 184, 11, 64, 192, 199, 245, 35, 126, 5, 227, 29 }, new byte[] { 9, 41, 206, 44, 101, 147, 82, 15, 78, 85, 47, 88, 242, 29, 165, 5, 36, 135, 125, 198, 118, 194, 170, 188, 136, 232, 198, 188, 167, 97, 38, 49, 114, 60, 205, 85, 53, 5, 253, 86, 79, 94, 82, 40, 248, 95, 24, 158, 164, 38, 25, 166, 201, 242, 222, 84, 184, 172, 22, 100, 233, 33, 157, 165, 178, 37, 32, 221, 8, 216, 139, 64, 149, 200, 212, 115, 210, 152, 203, 180, 243, 28, 88, 145, 1, 118, 247, 224, 33, 174, 57, 7, 193, 129, 143, 146, 149, 227, 210, 212, 43, 186, 6, 240, 70, 2, 9, 114, 94, 108, 47, 246, 53, 14, 231, 246, 248, 130, 80, 61, 169, 73, 156, 230, 63, 245, 158, 76 } });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "CharacterSkills",
                columns: new[] { "CharacterId", "SkillId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 1, 1, 20, "The Master Sword" });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CharacterId", "Damage", "Name" },
                values: new object[] { 2, 2, 5, "Crystal Wand" });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterSkills",
                keyColumns: new[] { "CharacterId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Characters",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 82, 107, 154, 99, 233, 89, 155, 156, 185, 75, 39, 25, 109, 226, 157, 226, 81, 71, 99, 221, 247, 199, 87, 238, 12, 197, 32, 61, 173, 128, 199, 250, 195, 213, 5, 38, 16, 17, 184, 204, 87, 53, 41, 248, 126, 193, 193, 138, 249, 82, 184, 11, 98, 61, 22, 241, 75, 49, 170, 127, 185, 109, 135, 225 }, new byte[] { 240, 8, 119, 103, 218, 31, 239, 179, 66, 4, 123, 93, 6, 24, 69, 86, 170, 31, 246, 250, 225, 42, 15, 13, 101, 238, 180, 231, 161, 43, 237, 72, 3, 212, 142, 140, 211, 87, 181, 21, 163, 230, 6, 107, 154, 174, 151, 78, 162, 87, 91, 174, 205, 126, 11, 201, 121, 124, 30, 111, 208, 7, 222, 45, 51, 66, 19, 170, 111, 11, 93, 218, 204, 232, 85, 232, 218, 92, 188, 136, 152, 53, 255, 73, 200, 53, 126, 55, 189, 237, 53, 64, 247, 124, 168, 97, 170, 220, 187, 87, 201, 198, 104, 119, 136, 182, 98, 187, 71, 208, 40, 197, 50, 31, 119, 136, 208, 200, 75, 77, 47, 106, 174, 144, 172, 65, 111, 241 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 82, 107, 154, 99, 233, 89, 155, 156, 185, 75, 39, 25, 109, 226, 157, 226, 81, 71, 99, 221, 247, 199, 87, 238, 12, 197, 32, 61, 173, 128, 199, 250, 195, 213, 5, 38, 16, 17, 184, 204, 87, 53, 41, 248, 126, 193, 193, 138, 249, 82, 184, 11, 98, 61, 22, 241, 75, 49, 170, 127, 185, 109, 135, 225 }, new byte[] { 240, 8, 119, 103, 218, 31, 239, 179, 66, 4, 123, 93, 6, 24, 69, 86, 170, 31, 246, 250, 225, 42, 15, 13, 101, 238, 180, 231, 161, 43, 237, 72, 3, 212, 142, 140, 211, 87, 181, 21, 163, 230, 6, 107, 154, 174, 151, 78, 162, 87, 91, 174, 205, 126, 11, 201, 121, 124, 30, 111, 208, 7, 222, 45, 51, 66, 19, 170, 111, 11, 93, 218, 204, 232, 85, 232, 218, 92, 188, 136, 152, 53, 255, 73, 200, 53, 126, 55, 189, 237, 53, 64, 247, 124, 168, 97, 170, 220, 187, 87, 201, 198, 104, 119, 136, 182, 98, 187, 71, 208, 40, 197, 50, 31, 119, 136, 208, 200, 75, 77, 47, 106, 174, 144, 172, 65, 111, 241 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
