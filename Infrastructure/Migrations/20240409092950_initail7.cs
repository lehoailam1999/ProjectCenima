﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initail7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms",
                column: "CinemaId",
                principalTable: "Cenimas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cenimas_CinemaId",
                table: "Rooms",
                column: "CinemaId",
                principalTable: "Cenimas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
