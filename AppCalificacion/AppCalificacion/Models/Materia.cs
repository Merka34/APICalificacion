using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppCalificacion.Models
{
    public partial class Materia
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int IdNombreMateria { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int IdAlumno { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Alumno IdAlumnoNavigation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Nombremateria IdNombreMateriaNavigation { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Calificacion Calificacion { get; set; }
    }
}
