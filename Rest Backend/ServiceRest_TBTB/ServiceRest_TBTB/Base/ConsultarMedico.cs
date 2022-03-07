using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using ServiceRest_TBTB.Database;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Base
{
    public class ConsultarMedico
    {
        private IConfiguration _configuration;
        public ConsultarMedico(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResponseConsultar ConsultarMedicoPorID (String id)
        {
            ResponseConsultar response = new ResponseConsultar();
            List<string> errores = new List<string>();

            try
            {
                #region Validar Campo ID

                //Se valida el campo id
                id = (!String.IsNullOrEmpty(id)) ? id.Trim() : "";
                Models.Validations.Id idVal = new Models.Validations.Id();
                ValidationResult resultRequest = idVal.Validate(id);
                errores.AddRange(resultRequest.Errors.Select(p => p.ErrorMessage).ToList());

                if (errores.Count > 0)
                {
                    response.codigo = 111;
                    response.mensaje = errores[0];
                    response.resultado = "Error";
                    response.medico = null;

                    return response;
                }
                #endregion Validar Campo ID
                TransaccionBaseDatos.ConsultarMedicoID(id, ref response, _configuration);

            }
            catch (Exception ex)
            {

                response.codigo = 103;
                response.mensaje = ex.Message;
                response.resultado = "Error al consultar medico";
            }

            return response;
        }

        public ResponseConsultarListaMedicos ConsultarListaMedicos()
        {
            ResponseConsultarListaMedicos response = new ResponseConsultarListaMedicos();
            List<string> errores = new List<string>();

            try
            {
                TransaccionBaseDatos.ConsultarListaMedicos(ref response, _configuration);
            }
            catch (Exception ex)
            {

                response.codigo = 103;
                response.mensaje = ex.Message;
                response.resultado = "Error al consultar lista de medicos";
            }

            return response;
        }
    }
}
