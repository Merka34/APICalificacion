
using APICalificacion.Models;
using APICalificacion.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICalificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        public AlumnosController(promedioContext context)
        {
            Context = context;
            repository = new Repository<Alumno>(Context);
        }
        Repository<Alumno> repository;

        public promedioContext Context { get; }

        [HttpGet]
        public IEnumerable<Alumno> Get()
        {
            return Context.Alumno.Include(x => x.Materia).ThenInclude(x => x.IdNombreMateriaNavigation);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var x = repository.Get(id);
            if (x == null)
            {
                return Ok(new Alumno());
            }
            return Ok(x);
        }

        [HttpPost]
        public IActionResult Post(Alumno a)
        {
            if (a == null) return BadRequest();

            if (string.IsNullOrWhiteSpace(a.NombreAlumno))
            {
                ModelState.AddModelError("", "Debe proporcionar el nombre del alumno");
            }
            if (Context.Alumno.Any(x => x.NombreAlumno == a.NombreAlumno))
            {
                ModelState.AddModelError("", "El nombre del alumno ya esta registrado");
            }
            if (ModelState.IsValid)
            {
                a.Id = 0;
                for (int i = 1; i <= Context.Nombremateria.Count(); i++)
                {
                    a.Materia.Add(new Materia { Calificacion = new Calificacion { P1 = 0, P2 = 0, P3 = 0 }, IdNombreMateria = i });
                }
                a.Usuarioalumno = new Usuarioalumno { Contrasena= "promedio"};
                Context.Alumno.Add(a);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        public IActionResult Put(Alumno a)
        {
            if (a == null) return BadRequest();

            if (string.IsNullOrWhiteSpace(a.NombreAlumno))
            {
                ModelState.AddModelError("", "Debe proporcionar el nombre del alumno");
            }

            if (Context.Alumno.Any(x => x.NombreAlumno == a.NombreAlumno && x.Id != a.Id))
            {
                ModelState.AddModelError("", "El nombre del alumno ya esta registrado");
            }

            if (ModelState.IsValid)
            {
                Alumno c = repository.Get(a.Id);
                c.NombreAlumno = a.NombreAlumno;
                repository.Update(c);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var x = Context.Alumno.Include(x => x.Materia).ThenInclude(x => x.Calificacion).Include(x=>x.Usuarioalumno).FirstOrDefault(x => x.Id == id);
            if (x == null)
            {
                ModelState.AddModelError("", "El alumno mencionado no existe o ya ha sido eliminado");
            }
            if (ModelState.IsValid)
            {
                foreach (Materia m in x.Materia)
                {
                    Context.Calificacion.Remove(m.Calificacion);
                    Context.Materia.Remove(m);
                }
                Context.Usuarioalumno.Remove(x.Usuarioalumno);
                Context.Alumno.Remove(x);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        public IActionResult Delete(Alumno a)
        {
            var x = Context.Alumno.Include(x=>x.Materia).ThenInclude(x=>x.Calificacion).FirstOrDefault(x=>x.Id==a.Id);
            if (x == null)
            {
                ModelState.AddModelError("", "El alumno mencionado no existe o ya ha sido eliminado");
            }
            if (ModelState.IsValid)
            {
                foreach (Materia m in x.Materia)
                {
                    Context.Calificacion.Remove(m.Calificacion);
                    Context.Materia.Remove(m);
                }
                Context.Usuarioalumno.Remove(x.Usuarioalumno);
                Context.Alumno.Remove(x);
                Context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
