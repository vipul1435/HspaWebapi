using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webApi.Migrations
{
    /// <inheritdoc />
    public partial class furnishedAded1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_FunrnishedTypes_FunrnishedTypeId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FunrnishedTypes",
                table: "FunrnishedTypes");

            migrationBuilder.RenameTable(
                name: "FunrnishedTypes",
                newName: "FurnishedTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnishedTypes",
                table: "FurnishedTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_FurnishedTypes_FunrnishedTypeId",
                table: "Properties",
                column: "FunrnishedTypeId",
                principalTable: "FurnishedTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_FurnishedTypes_FunrnishedTypeId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnishedTypes",
                table: "FurnishedTypes");

            migrationBuilder.RenameTable(
                name: "FurnishedTypes",
                newName: "FunrnishedTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FunrnishedTypes",
                table: "FunrnishedTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_FunrnishedTypes_FunrnishedTypeId",
                table: "Properties",
                column: "FunrnishedTypeId",
                principalTable: "FunrnishedTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
