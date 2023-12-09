using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Hateoas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles ="Admin")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private Hateoas.Hateoas Hateoas;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
            Hateoas = new Hateoas.Hateoas("localhost:5001/Produtos");
            Hateoas.AddAction("GET_INFO", "GET");
            Hateoas.AddAction("CREATE_PRODUCT", "POST");
            Hateoas.AddAction("EDIT_PRODUCT", "PUT");
            Hateoas.AddAction("DELETE_PRODUCT", "DELETE");
        }

        //Get: api/Produtos
        [HttpGet]
        public IActionResult GetProduto()
        {
            var produto = _context.Produtos.ToList();
            List <ProdutoContainer> produtoContainer = new List<ProdutoContainer>();
            foreach (var p in produto)
            {
                ProdutoContainer produtoHeateoas = new ProdutoContainer();
                produtoHeateoas.produto = p;
                produtoHeateoas.links = Hateoas.GetActions(p.Id);
                produtoContainer.Add(produtoHeateoas);
            }
            return Ok(produtoContainer);
        }

        //Get: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Produto>>>GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            ProdutoContainer produtoContainer = new ProdutoContainer();
            produtoContainer.produto = produto;
            produtoContainer.links = Hateoas.GetActions(produto.Id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(new{produto = produtoContainer.produto, link = produtoContainer.links});
        }

        //teste
        [HttpGet("test")]
        public IActionResult TesteClaims()
        {
            return Ok(HttpContext.User.Claims.First(claim => claim.Type.ToString().Equals("Nome do usuario", StringComparison.InvariantCultureIgnoreCase)).Value);
        }

        //Put: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<IActionResult>PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (id != produto.Id)
               {
                    return NotFound();
               }
               else
               {
                throw;
               }
            }

            return NoContent();
        }

        //Post: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produto>>PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new{ id = produto.Id }, produto);
        }

        //Delete: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            if (produto.Nome == null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class ProdutoContainer{
            public Produto produto{get; set;}
            public Link[] links{get; set;}
        }
    }
}