using Microsoft.Extensions.Configuration;
using ServiceRest_TBTB.Models.Request;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Base
{
    public class RegistrarMedico
    {
        private IConfiguration _configuration;

        public RegistrarMedico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response Registrar(RequestRegistrarMedico request)
        {
            Response response = new Response();
            
            try
            {
                Base.ValidarRequest cliente = new Base.ValidarRequest();
                response = cliente.ValidarRequestRegistar(request);

                if (response.codigo == 200)
                {
                    Database.TransaccionBaseDatos.ConsultarMedicoPorDocumento(request.numeroDocumento, ref response, _configuration);
                    
                    if (response.codigo == 200)
                    {
                        ResponseBD responseBD = Database.TransaccionBaseDatos.RegistrarMedicoBD(request, _configuration);

                        response.codigo = responseBD.codigo;
                        response.mensaje = responseBD.mensaje;
                        response.resultado = responseBD.resultado;
                        response.Errores = null;
                    }                   
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

