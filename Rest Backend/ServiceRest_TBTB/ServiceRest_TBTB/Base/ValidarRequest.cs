using FluentValidation.Results;
using ServiceRest_TBTB.Models.Request;
using ServiceRest_TBTB.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Base
{
    public class ValidarRequest
    {
        public Response ValidarRequestRegistar(RequestRegistrarMedico request)
        {
            Response response = new Response();
            response.Errores = new List<string>();

            #region Request
            Models.Validations.RequestRegistrarMedico entrada = new Models.Validations.RequestRegistrarMedico();
            ValidationResult resultRequest = entrada.Validate(request);
            response.Errores.AddRange(resultRequest.Errors.Select(p => p.ErrorMessage).ToList());

            if (response.Errores.Count > 0)
            {
                response.codigo = 111;
                response.mensaje = Utils.UtilitiesString.dCodeMessageEquivalence[response.codigo];
                response.resultado = "Error";
            }
            else
            {
                response.codigo = 200;
                response.Errores = null;
            }

            return response;
            #endregion Request
        }

        public Response ValidarRequestActualizar(RequestActualizarMedico request)
        {
            Response response = new Response();
            response.Errores = new List<string>();

            #region Request
            Models.Validations.RequestActualizarMedico entrada = new Models.Validations.RequestActualizarMedico();
            ValidationResult resultRequest = entrada.Validate(request);
            response.Errores.AddRange(resultRequest.Errors.Select(p => p.ErrorMessage).ToList());

            if (response.Errores.Count > 0)
            {
                response.codigo = 111;
                response.mensaje = Utils.UtilitiesString.dCodeMessageEquivalence[response.codigo];
                response.resultado = "Error";
            }
            else
            {
                response.codigo = 200;
                response.Errores = null;
            }

            return response;
            #endregion Request
        }
    }
}
