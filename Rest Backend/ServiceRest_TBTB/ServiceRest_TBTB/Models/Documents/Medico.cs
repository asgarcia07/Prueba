using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Models.Documents
{
    public class Medico
    {
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String numeroDocumento { get; set; }
        public String especialidad { get; set; }
        public String descripcion { get; set; }
    }
}
