using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiNux.Migrations
{
    public partial class material : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comission",
                table: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Validate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_MaterialId",
                table: "Suppliers",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Material_MaterialId",
                table: "Suppliers",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Material_MaterialId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_MaterialId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Suppliers");

            migrationBuilder.AddColumn<float>(
                name: "Comission",
                table: "Suppliers",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
