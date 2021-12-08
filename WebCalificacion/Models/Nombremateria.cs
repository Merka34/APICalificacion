using System;
using System.Collections.Generic;

#nullable disable

namespace WebCalificacion.Models
{
    public partial class Nombremateria
    {
        public Nombremateria()
        {
            Materia = new HashSet<Materia>();
        }

        public int Id { get; set; }
        public string NombreMateria1 { get; set; }

        public virtual ICollection<Materia> Materia { get; set; }
    }
}
