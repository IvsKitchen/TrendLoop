using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrendLoop.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWrongIdSubcategoryAttributeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "SubcategoriesAttributeTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SubcategoriesAttributeTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
