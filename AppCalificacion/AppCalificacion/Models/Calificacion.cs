using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace AppCalificacion.Models
{
    public partial class Calificacion
    {
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? P1 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? P2 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? P3 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Pf { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Materia IdNavigation { get; set; }
    }
}
