using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConvaReload.DataAccess.Migrations
{
    public partial class AuthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Conferences",
                newName: "ConferenceStatus");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "Users",
                newName: "HashedPassword");

            migrationBuilder.RenameColumn(
                name: "ConferenceStatus",
                table: "Conferences",
                newName: "Status");
        }
    }
}
