using MediatR;
using MinimalApi.People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.People.GetPeople
{
    public class GetPeopleRequestHandler : IRequestHandler<GetPeopleRequestHandler.Request, GetPeopleRequestHandler.Response>
    {
        private readonly IPeopleService _peopleService;

        public GetPeopleRequestHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var people = _peopleService.GetPeople();
            await Task.Delay(1000);

            return new Response
            {
                People = people.ToList(),
            };
        }

        public class Request : IRequest<Response>
        {

        }

        public class Response
        {
            public IList<Person> People { get; init; }
        }
    }
}
