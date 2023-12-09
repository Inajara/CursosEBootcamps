using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API1.Migrations
{
    public partial class PopularCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias(Nome, ImageUrl) Values('True Crime', 'warning.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome, ImageUrl) Values('Biografia', 'books.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome, ImageUrl) Values('Romance', 'blackrose.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
