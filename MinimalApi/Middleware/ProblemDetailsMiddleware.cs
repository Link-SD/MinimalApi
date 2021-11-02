using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Middleware
{
    public class ProblemDetailsMiddleware
    {
        internal static async Task WriteResponse(HttpContext context, bool isDevelopment)
        {
            using var scope = context.RequestServices.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ProblemDetailsMiddleware>>();
            logger.LogInformation("Current environment is Development: {isDevelopment}", isDevelopment);
           
            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            var problemDetails = GetAppropiateProblemDetails(errorFeature, context, isDevelopment);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status!.Value;

            context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
            };

            await context.Response.WriteAsJsonAsync<ProblemDetails>(problemDetails);
        }

        private static ProblemDetails GetAppropiateProblemDetails(IExceptionHandlerFeature errorFeature, HttpContext context, bool includeDetails)
        {
            var exception = errorFeature.Error;

            var problemDetails = new ProblemDetails
            {
                Type = $"https://example.com/problem-types/{exception.GetType().Name}",
                Title = includeDetails ? "An error occured: " + exception.Message : "An unexpected error occurred",
                Detail = includeDetails ? exception.ToString() : "Something went wrong",
                Instance = errorFeature switch
                {
                    ExceptionHandlerFeature e => e.Path,
                    _ => "unknown"
                },
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    ["trace"] = Activity.Current?.Id ?? context?.TraceIdentifier
                }
            };

            var specificDetails = CreateSpecificErrorDetails(exception, problemDetails);

            return specificDetails;
        }

        private static ProblemDetails CreateSpecificErrorDetails(Exception exception, ProblemDetails problemDetails)
        {
            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more validation errors occurred";
                    problemDetails.Detail = "The request contains invalid parameters. More information can be found in the errors.";
                    problemDetails.Extensions["errors"] = validationException.Errors
                                                                .GroupBy(x => x.PropertyName)
                                                                .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage)
                                                                .ToArray());
                    break;
            }

            return problemDetails;
        }
    }
}
