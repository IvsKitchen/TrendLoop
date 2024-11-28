using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrendLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceSubcategoryInAttributeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_AttributeTypes_AttributeTypeId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_AttributeTypeId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "AttributeTypeId",
                table: "Subcategories");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeTypeId",
                table: "Subcategories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_AttributeTypeId",
                table: "Subcategories",
                column: "AttributeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_AttributeTypes_AttributeTypeId",
                table: "Subcategories",
                column: "AttributeTypeId",
                principalTable: "AttributeTypes",
                principalColumn: "Id");
        }
    }
}
