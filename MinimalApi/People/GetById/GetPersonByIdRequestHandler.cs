using MediatR;
using MinimalApi.People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.People.GetById
{
    public class GetPersonByIdRequestHandler : IRequestHandler<GetPersonByIdRequestHandler.Request, GetPersonByIdRequestHandler.Response>
    {
        private readonly IPeopleService _peopleService;

        public GetPersonByIdRequestHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            var person = _peopleService.GetPersonById(request.Id);

            return new Response
            {
                Person = person,
            };
        }

        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {
            public Person Person { get; init; }
        }
    }

   
}
