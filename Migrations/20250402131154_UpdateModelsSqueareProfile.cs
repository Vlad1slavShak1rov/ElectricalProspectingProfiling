using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricalProspectingProfiling.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsSqueareProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ФотоПлощади",
                table: "Squares",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Фото_Профиля",
                table: "Profile",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ФотоПлощади",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "Фото_Профиля",
                table: "Profile");
        }
    }
}
