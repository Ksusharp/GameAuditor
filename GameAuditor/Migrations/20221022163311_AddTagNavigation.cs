﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAuditor.Migrations
{
    public partial class AddTagNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagNavigation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagNavigation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagNavigation_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagNavigation_PostId",
                table: "TagNavigation",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagNavigation");
        }
    }
}
