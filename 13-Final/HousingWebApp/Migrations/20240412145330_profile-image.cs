using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousingWebApp.Migrations
{
    /// <inheritdoc />
    public partial class profileimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AppUsers");
        }
    }
}
