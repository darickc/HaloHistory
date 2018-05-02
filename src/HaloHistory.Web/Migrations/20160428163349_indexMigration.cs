using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace HaloHistory.Web.Migrations
{
    public partial class indexMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MatchResultData_ItemId",
                table: "MatchResultData",
                column: "ItemId");
            migrationBuilder.CreateIndex(
                name: "IX_SpartanImageData_ItemId",
                table: "SpartanImageData",
                column: "ItemId");
            migrationBuilder.CreateIndex(
                name: "IX_EmblemImageData_ItemId",
                table: "EmblemImageData",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_MatchResultData_ItemId", table: "MatchResultData");
            migrationBuilder.DropIndex(name: "IX_SpartanImageData_ItemId", table: "SpartanImageData");
            migrationBuilder.DropIndex(name: "IX_EmblemImageData_ItemId", table: "EmblemImageData");
        }
    }
}
