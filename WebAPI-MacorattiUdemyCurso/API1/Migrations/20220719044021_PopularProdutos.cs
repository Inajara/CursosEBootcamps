using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API1.Migrations
{
    public partial class PopularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" + 
            "Values('BTK', 'Biografia sobre BTK', 69.90, 'btk.jpg', 10, now(), 2)");

            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" + 
            "Values('Lady Killers', 'Biografias sobre mulheres assassinas', 79.90, 'lk.jpg', 4, now(), 1)");

            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId)" + 
            "Values('O Silêncio dos Inocentes', 'Romance sobre personagem Hannibal Lecter', 59.90, 'hl.jpg', 3, now(), 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
