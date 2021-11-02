using FluentValidation;
using MinimalApi.People.CreatePerson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.People.CreatePerson
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonRequestHandler.Request>
    {
        public CreatePersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Age).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
