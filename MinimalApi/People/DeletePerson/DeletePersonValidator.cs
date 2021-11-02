using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.People.DeletePerson
{
    public class DeletePersonValidator : AbstractValidator<DeletePersonRequestHandler.Request>
    {
        public DeletePersonValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
