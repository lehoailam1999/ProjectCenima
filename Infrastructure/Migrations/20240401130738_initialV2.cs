using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Rooms_RoomId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_SeatTypes_SeatTypeId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_SeatTypeId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "SeatTypeId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_BillTickets_TicketId",
                table: "BillTickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillTickets_Tickets_TicketId",
                table: "BillTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillTickets_Tickets_TicketId",
                table: "BillTickets");

            migrationBuilder.DropIndex(
                name: "IX_BillTickets_TicketId",
                table: "BillTickets");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatTypeId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomId",
                table: "Rooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_SeatTypeId",
                table: "Rooms",
                column: "SeatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Rooms_RoomId",
                table: "Rooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_SeatTypes_SeatTypeId",
                table: "Rooms",
                column: "SeatTypeId",
                principalTable: "SeatTypes",
                principalColumn: "Id");
        }
    }
}
