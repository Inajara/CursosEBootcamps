using API1.Models;
using Microsoft.EntityFrameworkCore;

namespace API1.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options )
        {}

        public DbSet<Categoria>? Categorias{get; set;}
        public DbSet<Produto>? Produtos{get; set;}
    }
}