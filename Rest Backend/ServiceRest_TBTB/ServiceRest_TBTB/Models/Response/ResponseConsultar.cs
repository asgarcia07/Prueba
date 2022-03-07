using ServiceRest_TBTB.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Models.Response
{
    public class ResponseConsultar
    {
        public int codigo { get; set; }

        public String mensaje { get; set; }

        public String resultado { get; set; }

        public List<String> Errores { get; set; }

        public Medico medico { get; set; }
    }
}
