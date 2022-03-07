using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Models.Response
{
    public class Response
    {
        public int codigo { get; set; }

        public String mensaje { get; set; }

        public String resultado { get; set; }

        public List<String> Errores { get; set; }
    }
}
