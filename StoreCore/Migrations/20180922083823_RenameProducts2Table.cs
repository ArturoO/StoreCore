﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreCore.Migrations
{
    public partial class RenameProducts2Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products2",
                table: "Products2");

            migrationBuilder.RenameTable(
                name: "Products2",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products2",
                table: "Products2",
                column: "Id");
        }
    }
}
