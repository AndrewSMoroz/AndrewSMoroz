using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AndrewSMoroz.Data.Migrations
{
    public partial class ContactsAddRecruiter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecruiterCompanyID",
                table: "Position",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecruiter",
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Position_RecruiterCompanyID",
                table: "Position",
                column: "RecruiterCompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_RecruiterCompanyID",
                table: "Position",
                column: "RecruiterCompanyID",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_RecruiterCompanyID",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_RecruiterCompanyID",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "RecruiterCompanyID",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "IsRecruiter",
                table: "Company");
        }
    }
}
