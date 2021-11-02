using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.People.DeletePerson
{
    public class DeletePersonRequestHandler : IRequestHandler<DeletePersonRequestHandler.Request, DeletePersonRequestHandler.Response>
    {
        private readonly IPeopleService _peopleService;

        public DeletePersonRequestHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            _peopleService.DeletePerson(request.Id);
            await Task.Delay(1000);
            return new Response();
        }

        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {

        }
    }
}
