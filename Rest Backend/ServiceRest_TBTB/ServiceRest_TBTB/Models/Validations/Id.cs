using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRest_TBTB.Models.Validations
{
    public class Id: AbstractValidator<string>
    {
        public Id()
        {
            RuleFor(x => x).NotEmpty().Matches(Utils.UtilitiesString.regexID).WithName("id");
        }

        public ValidationResult Validate(Id id)
        {
            ValidationResult result = new ValidationResult();
            result = Validate(id);
            return result;
        }
    }
}
