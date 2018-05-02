using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Celestial.Migrations
{
    public partial class AddUserToPlanet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stability",
                table: "Stats",
                newName: "Stability");

            migrationBuilder.RenameColumn(
                name: "pop",
                table: "Stats",
                newName: "Pop");

            migrationBuilder.RenameColumn(
                name: "crystal",
                table: "Stats",
                newName: "Crystal");

            migrationBuilder.RenameColumn(
                name: "capital",
                table: "Stats",
                newName: "Capital");

            migrationBuilder.RenameColumn(
                name: "government",
                table: "Planets",
                newName: "Government");

            migrationBuilder.RenameColumn(
                name: "geography",
                table: "Planets",
                newName: "Geography");

            migrationBuilder.RenameColumn(
                name: "economy",
                table: "Planets",
                newName: "Economy");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Planets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_UserId",
                table: "Planets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_AspNetUsers_UserId",
                table: "Planets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_AspNetUsers_UserId",
                table: "Planets");

            migrationBuilder.RenameColumn(
                name: "Stability",
                table: "Stats",
                newName: "stability");

            migrationBuilder.RenameColumn(
                name: "Pop",
                table: "Stats",
                newName: "pop");

            migrationBuilder.RenameColumn(
                name: "Crystal",
                table: "Stats",
                newName: "crystal");

            migrationBuilder.RenameColumn(
                name: "Capital",
                table: "Stats",
                newName: "capital");

            migrationBuilder.RenameColumn(
                name: "Government",
                table: "Planets",
                newName: "government");

            migrationBuilder.RenameColumn(
                name: "Geography",
                table: "Planets",
                newName: "geography");

            migrationBuilder.RenameColumn(
                name: "Economy",
                table: "Planets",
                newName: "economy");

            migrationBuilder.DropIndex(
                name: "IX_Planets_UserId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Planets");
        }
    }
}
