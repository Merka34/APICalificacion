using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCalificacion.Models;

namespace WebCalificacion.Controllers
{
    [Authorize(Roles ="Docente")]
    [Area("Docente")]
    public class AlumnosController : Controller
    {
        public string URL { get; set; } = "https://localhost:44397";
        public async Task<IActionResult> Index()
        {
            WebClient webClient = new WebClient();
            string listaAlumnos = await webClient.DownloadStringTaskAsync($"{URL}/api/alumnos");
            if (!string.IsNullOrWhiteSpace(listaAlumnos))
            {
                var AlumnoList = JsonConvert.DeserializeObject<IEnumerable<Alumno>>(listaAlumnos);
                return View(AlumnoList);
            }
            else
            {
                ModelState.AddModelError("", "Hubo problemas cuando se intento obtener la lista de alumnos");
                List<Alumno> listaAl = new List<Alumno>();
                IEnumerable<Alumno> listaAlumno = (IEnumerable<Alumno>)listaAl;
                return View(listaAlumno);
            }
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                ModelState.AddModelError("", "Agrege el nombre del alumno");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            Alumno alu = new Alumno { NombreAlumno = nombre };
            string json = JsonConvert.SerializeObject(alu);
            HttpClient httpClient = new HttpClient();
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{URL}/api/alumnos/", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            string message = await httpResponse.Content.ReadAsStringAsync();
            ModelState.AddModelError("", message);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            WebClient webClient = new WebClient();
            string alumno = await webClient.DownloadStringTaskAsync($"{URL}/api/alumnos/{id}");
            Alumno al = JsonConvert.DeserializeObject<Alumno>(alumno);
            if (al.Id==0)
            {
                return RedirectToAction("Index");
            }
            return View(al);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Alumno al)
        {
            if (string.IsNullOrWhiteSpace(al.NombreAlumno))
            {
                ModelState.AddModelError("", "Debe agregar un nombre al alumno");
            }
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(al);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var res = await httpClient.PutAsync($"{URL}/api/alumnos/", httpContent);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                string message = await res.Content.ReadAsStringAsync();
                ModelState.AddModelError("", message);
            }
            return View(al);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            WebClient webClient = new WebClient();
            string alumno = webClient.DownloadString($"{URL}/api/alumnos/{id}");
            Alumno al = JsonConvert.DeserializeObject<Alumno>(alumno);
            if (al.Id==0)
            {
                return RedirectToAction("Index");
            }
            return View(al);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(Alumno al)
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.DeleteAsync($"{URL}/api/alumnos/{al.Id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            string message = await httpResponse.Content.ReadAsStringAsync();
            ModelState.AddModelError("", message);
            return View(al);
        }

        public IActionResult Calificacion(int id)
        {
            WebClient webClient = new WebClient();
            string cali = webClient.DownloadString($"{URL}/api/calificaciones/{id}");
            if (!string.IsNullOrWhiteSpace(cali))
            {
                var cal = JsonConvert.DeserializeObject<Calificacion>(cali);
                return View(cal);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Calificacion(Calificacion Cal)
        {
            HttpClient httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(Cal);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var httpRequest = await httpClient.PutAsync($"{URL}/api/calificaciones/", httpContent);
            if (httpRequest.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            string message = await httpRequest.Content.ReadAsStringAsync();
            ModelState.AddModelError("", message);
            return RedirectToAction("Index");
        }
    }
}
