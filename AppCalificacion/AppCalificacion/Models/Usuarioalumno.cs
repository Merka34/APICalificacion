using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppCalificacion.Models
{
    public partial class Usuarioalumno
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int IdAlumno { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Contrasena { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Alumno IdAlumnoNavigation { get; set; }
    }
}
