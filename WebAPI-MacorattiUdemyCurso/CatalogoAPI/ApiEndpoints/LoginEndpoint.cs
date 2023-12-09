using CatalogoAPI.Models;
using CatalogoAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoAPI.ApiEndpoints
{
    public static class LoginEndpoint
    {
        public static void MapLoginEndpoints(this WebApplication app)
        {
            //endpoint login
            app.MapPost("/login", [AllowAnonymous] (Usuario usuario, ITokenService tokenService) => {
                if(usuario == null){
                    return Results.BadRequest("Login Inválido");
                }
                if(usuario.NomeUsuario == "devtest" && usuario.SenhaUsuario == "senha@1234"){
                    var tokenString = tokenService.GeraToken(app.Configuration["Jwt:Key"],
                                                            app.Configuration["Jwt:Issuer"], 
                                                            app.Configuration["Jwt:Audience"],
                                                            usuario);
                    return Results.Ok(new { token = tokenString });
                }else{
                    return Results.BadRequest("Login inválido");
                }
            }).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticacao");
        }
    }
}