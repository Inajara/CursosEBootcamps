using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Hateoas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        //Get: api/Usuarios
        [HttpGet]
        public IActionResult GetUsuario()
        {
            var usuario = _context.Usuarios.ToList();
            List <UsuarioContainer> usuarioContainer = new List<UsuarioContainer>();
            foreach (var u in usuario)
            {
                UsuarioContainer usuarioHeateoas = new UsuarioContainer();
                usuarioHeateoas.usuario = u;
                usuarioContainer.Add(usuarioHeateoas);
            }
            return Ok(usuarioContainer);
        }

        //Get: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>>GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            UsuarioContainer usuarioContainer = new UsuarioContainer();
            usuarioContainer.usuario = usuario;
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(new{usuario = usuarioContainer.usuario, link = usuarioContainer.links});
        }

        //Put: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult>PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (id != usuario.Id)
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

        //Post: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>>PostUsuario(Usuario usuario)
        {
            //verificar se credenciais sao validas
            //verificar se email ja foi cadastrado
            //encriptar senha
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new{ id = usuario.Id }, usuario);
        }

        //Delete: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (usuario.Nome == null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class UsuarioContainer{
            public Usuario usuario{get; set;}
            public Link[] links{get; set;}
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario login)
        {
            //buscar por email
            try
            {
                Usuario usuario = _context.Usuarios.First(u => u.Nome.Equals(login.Nome));
                if (usuario != null)
                {
                    //encontrou cadastro valido
                    if (usuario.Senha.Equals(login.Senha))
                    {
                        //senha está correta
                        //chave de seguranca
                        string ChaveSeguranca = "Essas_Sao_Pessoas_Com_Mentes_Diabolicas";
                        var ChaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ChaveSeguranca));
                        var CredenciaisDeAcesso = new SigningCredentials(ChaveSimetrica, SecurityAlgorithms.HmacSha256Signature);
                        //dados do usuario
                        var claims = new List<Claim>();
                        claims.Add(new Claim("Id do usuario", usuario.Id.ToString()));
                        claims.Add(new Claim("Nome do usuario", usuario.Nome));
                        claims.Add(new Claim("Email do usuario", usuario.Email));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        var JWT = new JwtSecurityToken(
                            issuer: "btk.com", //quem fornece
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: CredenciaisDeAcesso,
                            claims: claims
                        );
                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));//gerar token
                    }else
                    {
                        //senhas não conferem
                        Response.StatusCode = 401;
                        return new ObjectResult("");
                    }
                }else
                {
                    //nao encontrou cadstro valido; não existe usuario
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = 401;
                return new ObjectResult("");
            }
            //verificar senha
            //se valido, gerar e retornar token jwt
        }
    }
}