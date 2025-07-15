using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoProductos_Productos_ProductoId",
                table: "CarritoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Carritos_CarritoId",
                table: "Compras");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoProductos_Productos_ProductoId",
                table: "CarritoProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Carritos_CarritoId",
                table: "Compras",
                column: "CarritoId",
                principalTable: "Carritos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoProductos_Productos_ProductoId",
                table: "CarritoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Carritos_CarritoId",
                table: "Compras");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoProductos_Productos_ProductoId",
                table: "CarritoProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Carritos_CarritoId",
                table: "Compras",
                column: "CarritoId",
                principalTable: "Carritos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
