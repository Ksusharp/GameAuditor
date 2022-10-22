using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAuditor.Migrations
{
    public partial class FixTagModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagType",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagType",
                table: "TagNavigation");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TagNavigation_TagId",
                table: "TagNavigation",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagNavigation_Tags_TagId",
                table: "TagNavigation",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagNavigation_Tags_TagId",
                table: "TagNavigation");

            migrationBuilder.DropIndex(
                name: "IX_TagNavigation_TagId",
                table: "TagNavigation");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TagType",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagType",
                table: "TagNavigation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
