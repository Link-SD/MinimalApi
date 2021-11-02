using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Helpers.Endpoints;
using MinimalApi.People.CreatePerson;
using MinimalApi.People.CreatePerson.Models;
using MinimalApi.People.DeletePerson;
using MinimalApi.People.GetById;
using MinimalApi.People.GetPeople;
using MinimalApi.People.Models;
using MinimalApi.People.UpdatePerson;
using MinimalApi.People.UpdatePerson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.People.Endpoints
{
    public class PeopleEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/api/people", GetPeople);
            app.MapGet("/api/people/{id}", GetPersonById);

            app.MapPost("/api/people/", CreatePerson);
            app.MapPut("/api/people/{id}", UpdatePerson);

            app.MapDelete("/api/people/{id}", DeletePerson);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private async Task<IResult> CreatePerson(IMediator mediator, CreatePersonModel createPersonModel)
        {
            var result = await mediator.Send(new CreatePersonRequestHandler.Request { Name = createPersonModel.Name, Age = createPersonModel.Age });
            return Results.Created($"/api/people/{result.Person.Id}", result.Person);
        }

        private async Task<IResult> GetPeople(IMediator mediator)
        {
            var result = await mediator.Send(new GetPeopleRequestHandler.Request());

            return Results.Ok(result.People);
        }

        private async Task<IResult> GetPersonById(IMediator mediator, Guid id)
        {
            var result = await mediator.Send(new GetPersonByIdRequestHandler.Request { Id = id });

            if (result.Person is null)
                return Results.NotFound(id);

            return Results.Ok(result.Person);
        }

        private async Task<IResult> DeletePerson(IMediator mediator, Guid id)
        {
            await mediator.Send(new DeletePersonRequestHandler.Request { Id = id });
            return Results.Ok();
        }

        private async Task<IResult> UpdatePerson(IMediator mediator, UpdatePersonModel updatePersonModel)
        {
            var result = await mediator.Send(new UpdatePersonRequestHandler.Request { Id = updatePersonModel.Id, Age = updatePersonModel.Age, Name = updatePersonModel.Name });
            return Results.Ok(result.Person);
        }


        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPeopleService, PeopleService>();
        }
    }
}