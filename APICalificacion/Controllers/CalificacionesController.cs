using APICalificacion.Models;
using APICalificacion.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APICalificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        public CalificacionesController(promedioContext context)
        {
            Context = context;
            repository = new Repository<Calificacion>(Context);
        }
        Repository<Calificacion> repository;

        public promedioContext Context { get; }

        [HttpGet("{id}")]
        public Calificacion GetByID(int id)
        {
            var calificacion = Context.Calificacion.Include(x=>x.IdNavigation)
                .ThenInclude(x=>x.IdNombreMateriaNavigation)
                .Include(x=>x.IdNavigation.IdAlumnoNavigation)
                .FirstOrDefault(x=>x.Id==id);
            
            if (calificacion==null)
            {
                return new Calificacion();
            }
            return calificacion;
        }

        [HttpGet("list/{id}")]
        public List<Calificacion> GetID(int id)
        {
            List<Calificacion> calificaciones = Context.Calificacion.Include(x => x.IdNavigation)
                .ThenInclude(x => x.IdNombreMateriaNavigation)
                .Where(x => x.IdNavigation.IdAlumno == id).ToList();
            return calificaciones;
        }

        [HttpGet]
        public IEnumerable<Nombremateria> GetNombreMaterias()
        {
            return Context.Nombremateria;
        }

        [HttpPut]
        public IActionResult Put(Calificacion c)
        {
            var cal = Context.Calificacion.FirstOrDefault(x=>x.Id == c.Id);
            if (cal==null)
            {
                ModelState.AddModelError("", "No se encontro");
            }

            if (ModelState.IsValid)
            {
                cal.P1 = c.P1;
                cal.P2 = c.P2;
                cal.P3 = c.P3;
                cal.Pf = (double)(cal.P1 + cal.P2 + cal.P3) / 3;
                Context.Update(cal);
                Context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
