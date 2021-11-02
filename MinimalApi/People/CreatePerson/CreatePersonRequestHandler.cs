using MediatR;
using MinimalApi.People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.People.CreatePerson
{
    public class CreatePersonRequestHandler : IRequestHandler<CreatePersonRequestHandler.Request, CreatePersonRequestHandler.Response>
    {
        private readonly IPeopleService _peopleService;

        public CreatePersonRequestHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var personToCreate = new Person
            {
                Name = request.Name,
                Age = request.Age,
            };

            var person = _peopleService.CreatePerson(personToCreate);
            await Task.Delay(1000);

            return new Response
            {
                Person = person
            };
        }

        public class Request: IRequest<Response>
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Response
        {
            public Person Person { get; init; }
        }
    }
}
