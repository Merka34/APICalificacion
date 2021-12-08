using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace AppCalificacion.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            Materia = new HashSet<Materia>();
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NombreAlumno { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Usuarioalumno Usuarioalumno { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Materia> Materia { get; set; }
    }
}
