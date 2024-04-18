using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialv9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms",
                column: "CinemaId",
                principalTable: "Cenimas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Schedules",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms",
                column: "CinemaId",
                principalTable: "Cenimas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
