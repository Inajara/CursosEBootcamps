using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoAPI.Models;

namespace CatalogoAPI.Services
{
    public interface ITokenService
    {
        string GeraToken(string key, string issuer, string audience, Usuario usuario);
    }
}