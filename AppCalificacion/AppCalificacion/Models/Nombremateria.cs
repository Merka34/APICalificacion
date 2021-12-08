using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppCalificacion.Models
{
    public partial class Nombremateria
    {
        public Nombremateria()
        {
            Materia = new HashSet<Materia>();
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NombreMateria1 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ICollection<Materia> Materia { get; set; }
    }
}
