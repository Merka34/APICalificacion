using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppCalificacion.Models
{
    public partial class Usuariodocente
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Nombre { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Contrasena { get; set; }
    }
}
