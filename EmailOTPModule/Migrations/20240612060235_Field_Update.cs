using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailOTPModule.Migrations
{
    public partial class Field_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "UserOTPLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "UserOTPLogs");
        }
    }
}
