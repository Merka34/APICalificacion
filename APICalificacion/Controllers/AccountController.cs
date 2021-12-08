using APICalificacion.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APICalificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(promedioContext context)
        {
            Context = context;
        }

        public promedioContext Context { get; }

        [HttpGet("alumno/{nombre}/{password}")]
        public Usuarioalumno LoginAlumno(string nombre, string password)
        {
            var cuenta = Context.Usuarioalumno.FirstOrDefault(x=>x.IdAlumnoNavigation.NombreAlumno==nombre && x.Contrasena==password);
            if (cuenta==null)
            {
                return new Usuarioalumno();
            }
            return cuenta;
        }

        [HttpGet("docente/{nombre}/{password}")]
        public ClaimsIdentity LoginDocente(string nombre, string password)
        {
            var cuenta = Context.Usuariodocente.FirstOrDefault(x => x.Nombre == nombre && x.Contrasena == password);
            if (cuenta == null)
            {
                return null;
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, nombre));
            claims.Add(new Claim(ClaimTypes.Role, "Docente"));
            var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return identidad;
        }
    }
}
