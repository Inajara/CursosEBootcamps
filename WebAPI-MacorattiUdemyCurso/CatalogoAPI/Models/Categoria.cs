using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatalogoAPI.Models
{
    public class Categoria
    {
        public int Id {get; set;}
        public string? Nome {get; set;}
        public string? Descricao {get; set;}

        [JsonIgnore]
        public ICollection<Produto>? Produtos {get; set;}
    }
}