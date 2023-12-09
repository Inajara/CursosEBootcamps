using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Produto>? Produtos {get; set;}
        public DbSet<Categoria>? Categorias {get; set;}

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //categorias
            mb.Entity<Categoria>().HasKey(c => c.Id);

            mb.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();

            mb.Entity<Categoria>().Property(c => c.Descricao).HasMaxLength(150).IsRequired();

            //produtos
            mb.Entity<Produto>().HasKey(p => p.Id);

            mb.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();

            mb.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(150).IsRequired();

            mb.Entity<Produto>().Property(p => p.Imagem).HasMaxLength(100);

            mb.Entity<Produto>().Property(p => p.Preco).HasPrecision(14, 2);

            //relacionamentos
            mb.Entity<Produto>().HasOne<Categoria>(c => c.Categoria).WithMany(p => p.Produtos).HasForeignKey(c => c.CategoriaId);
        }
    }
}