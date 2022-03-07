using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceRest_TBTB.Models.Data
{
    public partial class MedicoEspecialidad
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdEspecialidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Creacion { get; set; }
        public DateTime? Actualizacion { get; set; }

        public virtual Medico IdMedicoNavigation { get; set; }
    }
}
