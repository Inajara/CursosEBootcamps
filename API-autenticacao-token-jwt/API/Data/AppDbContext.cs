using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Produto>Produtos{get; set;}
        public DbSet<Usuario>Usuarios{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
            .Property(p => p.Nome).HasMaxLength(80);

            modelBuilder.Entity<Produto>()
            .Property(p => p.Preco).HasPrecision(10, 2);

            modelBuilder.Entity<Produto>()
            .HasData(
                new Produto{Id = 1, Nome = "Caderno", Preco = 7.95M, Estoque = 10, Status = true},
                new Produto{Id = 2, Nome = "Caneta", Preco = 2.45M, Estoque = 30, Status = true},
                new Produto{Id = 3, Nome = "Estojo", Preco = 5.00M, Estoque = 15, Status = true}
            );

            modelBuilder.Entity<Usuario>()
            .Property(u => u.Nome).HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
            .HasData(
                new Usuario{Id = 1, Nome = "Admin", Email = "admin@gft.com", Senha = "Gft@1234", Status = true}
            );
        }
    }
}