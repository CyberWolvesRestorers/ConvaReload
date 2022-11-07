using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConvaReload.DataAccess.Migrations
{
    public partial class ConferenceStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Conferences",
                newName: "ConferenceStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConferenceStatus",
                table: "Conferences",
                newName: "Status");
        }
    }
}
