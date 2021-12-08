using System;
using System.Collections.Generic;

#nullable disable

namespace APICalificacion.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            Materia = new HashSet<Materia>();
        }

        public int Id { get; set; }
        public string NombreAlumno { get; set; }

        public virtual Usuarioalumno Usuarioalumno { get; set; }
        public virtual ICollection<Materia> Materia { get; set; }
    }
}
