using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MinimalApi.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseProblemDetails(this IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
                builder.Use(WriteDevelopmentMessage);
            else
                builder.Use(WriteProductionMessage);
            return builder;
        }

        private static Task WriteDevelopmentMessage(HttpContext httpContext, Func<Task> next) => ProblemDetailsMiddleware.WriteResponse(httpContext, isDevelopment: true);
        private static Task WriteProductionMessage(HttpContext httpContext, Func<Task> next) => ProblemDetailsMiddleware.WriteResponse(httpContext, isDevelopment: false);
    }
}
