using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acme.ToDo.Data.Migrations
{
    public partial class acmetodoinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "acme_ToDoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acme_ToDoItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_acme_ToDoItems_SiteId",
                table: "acme_ToDoItems",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_acme_ToDoItems_UserId",
                table: "acme_ToDoItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acme_ToDoItems");
        }
    }
}
