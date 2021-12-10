using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebCalificacion.Controllers
{
    public class HomeController : Controller
    {
        public string URL { get; set; } = "https://localhost:44397";

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Docente" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nombre, string password)
        {
            WebClient web = new WebClient();
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Por favor ingrese nombre de usuario o contraseña");
                return View();
            }
            string cuenta = web.DownloadString($"{URL}/api/Account/docente/{nombre}/{password}");
            if (!string.IsNullOrWhiteSpace(cuenta))
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, nombre)); 
                claims.Add(new Claim(ClaimTypes.Role, "Docente"));
                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(identidad));
                return RedirectToAction("Index", "Home", new { area = "Docente" });
            }
            ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public string AccessDenied()
        {
            return "Acceso Denegado";
        }
    }
}
