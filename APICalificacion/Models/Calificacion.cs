using System;
using System.Collections.Generic;

#nullable disable

namespace APICalificacion.Models
{
    public partial class Calificacion
    {
        public int Id { get; set; }
        public int? P1 { get; set; }
        public int? P2 { get; set; }
        public int? P3 { get; set; }
        public double? Pf { get; set; }

        public virtual Materia IdNavigation { get; set; }
    }
}
