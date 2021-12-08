using System;
using System.Collections.Generic;

#nullable disable

namespace APICalificacion.Models
{
    public partial class Materia
    {
        public int Id { get; set; }
        public int IdNombreMateria { get; set; }
        public int IdAlumno { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
        public virtual Nombremateria IdNombreMateriaNavigation { get; set; }
        public virtual Calificacion Calificacion { get; set; }
    }
}
