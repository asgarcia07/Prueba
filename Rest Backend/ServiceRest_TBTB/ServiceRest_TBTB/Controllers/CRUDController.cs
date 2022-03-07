using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceRest_TBTB.Models.Request;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CRUDController : ControllerBase
    {
        private IConfiguration _configuration;

        public CRUDController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("api/RegistrarMedico")]
        public Response RegistarMedico([FromBody] RequestRegistrarMedico request)
        {
            Base.RegistrarMedico cliente = new Base.RegistrarMedico(_configuration);
            Response response = cliente.Registrar(request);
            return response;
        }

        [HttpGet("api/ConsultarListadoMedicos")]
        public ResponseConsultarListaMedicos ConsultarListaMedicos()
        {
            Base.ConsultarMedico cliente = new Base.ConsultarMedico(_configuration);
            ResponseConsultarListaMedicos response = cliente.ConsultarListaMedicos();
            return response;
        }

        [HttpGet("api/ConsultarMedico/{id}")]
        public ResponseConsultar ConsultarMedicoPorID([FromRoute] String id)
        {
            Base.ConsultarMedico cliente = new Base.ConsultarMedico(_configuration);
            ResponseConsultar response = cliente.ConsultarMedicoPorID(id);
            return response;
        }

        [HttpPost("api/ActualizarMedico")]
        public Response ActualizarMedico([FromBody] RequestActualizarMedico request)
        {
            Base.ActualizarMedico cliente = new Base.ActualizarMedico(_configuration);
            Response response = cliente.Actualizar(request);
            return response;
        }

        [HttpDelete("api/EliminarMedico/{id}")]
        public Response EliminarMedicoPorID([FromRoute] String id)
        {
            Base.EliminarMedico cliente = new Base.EliminarMedico(_configuration);
            Response response = cliente.EliminarMedicoPorID(id);
            return response;
        }       

    }
}
