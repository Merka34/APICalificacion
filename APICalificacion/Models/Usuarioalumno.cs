﻿using System;
using System.Collections.Generic;

#nullable disable

namespace APICalificacion.Models
{
    public partial class Usuarioalumno
    {
        public int IdAlumno { get; set; }
        public string Contrasena { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; }
    }
}
