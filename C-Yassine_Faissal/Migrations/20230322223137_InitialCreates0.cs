using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace C_Yassine_Faissal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreates0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGuest",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGuest",
                table: "User");
        }
    }
}
