using MediatR;
using MinimalApi.People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.People.UpdatePerson
{
    public class UpdatePersonRequestHandler : IRequestHandler<UpdatePersonRequestHandler.Request, UpdatePersonRequestHandler.Response>
    {
        private readonly IPeopleService _peopleService;

        public UpdatePersonRequestHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var personToUpdate = new Person
            {
                Id = request.Id,
                Name = request.Name,
                Age = request.Age,
            };

            var person = _peopleService.UpdatePerson(personToUpdate);
            await Task.Delay(1000);

            return new Response
            {
                Person = person
            };
        }

        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Response
        {
            public Person Person { get; init; }
        }
    }
}
