using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCalificacion.Areas.Docente.Controllers
{
    [Authorize(Roles = "Docente")]
    [Area("Docente")]
    public class HomeController : Controller
    {
        [Route("docente")]
        [Route("docente/Home")]
        [Route("docente/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
