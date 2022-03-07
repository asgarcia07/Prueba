using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceRest_TBTB.Models.Data
{
    public partial class Medico
    {
        public Medico()
        {
            MedicoEspecialidads = new HashSet<MedicoEspecialidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? Creacion { get; set; }
        public DateTime? Actualizacion { get; set; }

        public virtual ICollection<MedicoEspecialidad> MedicoEspecialidads { get; set; }
    }
}
