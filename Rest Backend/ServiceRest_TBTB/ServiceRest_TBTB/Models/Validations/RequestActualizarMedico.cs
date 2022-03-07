using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Models.Validations
{
    public class RequestActualizarMedico: AbstractValidator<Request.RequestActualizarMedico>
    {
        public RequestActualizarMedico()
        {
            RuleFor(x => x.idMedico).NotEmpty().Matches(Utils.UtilitiesString.regexID).WithName("id");
            RuleFor(x => x.nombre).NotNull().WithMessage("El campo nombre no puede ser nulo");
            RuleFor(x => x.nombre).Matches(Utils.UtilitiesString.regexNombre);
            RuleFor(x => x.apellido).NotNull().WithMessage("El campo apellido no puede ser nulo");
            RuleFor(x => x.apellido).Matches(Utils.UtilitiesString.regexNombre);
            RuleFor(x => x.numeroDocumento).NotNull().WithMessage("El campo numeroDocumento no puede ser nulo");
            RuleFor(x => x.numeroDocumento).Matches(Utils.UtilitiesString.regexNumeroDocumento);
            RuleFor(x => x.especialidad).NotNull().WithMessage("El campo especialidad no puede ser nulo");
            RuleFor(x => x.especialidad).Matches(Utils.UtilitiesString.regexTipoEspecialidad);
            RuleFor(x => x.descripcion).Matches(Utils.UtilitiesString.regexTextoLibre);
        }
    }
}
