using Microsoft.Extensions.Configuration;
using ServiceRest_TBTB.Models.Request;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Base
{
    public class ActualizarMedico
    {
        private IConfiguration _configuration;

        public ActualizarMedico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response Actualizar(RequestActualizarMedico request)
        {
            Response response = new Response();

            try
            {
                Base.ValidarRequest cliente = new Base.ValidarRequest();
                response = cliente.ValidarRequestActualizar(request);

                if (response.codigo == 200)
                {
                    Database.TransaccionBaseDatos.ActualizarMedicoBD(request, _configuration, ref response);
                }
            }
            catch (Exception ex)
            {
                response.codigo = 103;
                response.mensaje = Utils.UtilitiesString.dCodeMessageEquivalence[response.codigo];
                response.resultado = "Error";
            }

            return response;
        }
    }
}
