using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace API1.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}