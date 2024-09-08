﻿using Microsoft.AspNetCore.Diagnostics;
using MyApi.DTOs.CommonDtos;
using MyApi.Exceptions;
using System.Net;

namespace MyApi.Extensions
{
    public static class ExceptionHandlerServiceExtension
    {
        public static IApplicationBuilder AddExcepitonHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                    string message = "Unexpected error occured";

                    if (feature.Error is IBaseException)
                    {
                        var exception = (IBaseException)feature.Error;
                        statusCode = exception.StatusCode;
                        message = exception.ErrorMessage;
                    }

                    var response = new ResponseDto
                    {
                        StatusCode = (int)statusCode,
                        Message = message
                    };

                    context.Response.StatusCode = (int)statusCode;
                    await context.Response.WriteAsJsonAsync(response);
                    await context.Response.CompleteAsync();
                });
            });

            return application;
        }
    }
}
