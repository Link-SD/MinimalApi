using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.People.UpdatePerson
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonRequestHandler.Request>
    {
        public UpdatePersonValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Age).GreaterThanOrEqualTo(0);
        }
    }
}
