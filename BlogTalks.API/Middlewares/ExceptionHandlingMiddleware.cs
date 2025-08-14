﻿using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Exceptions.BlogTalks.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogTalks.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred",
            };

            if (exception.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exception.Errors
                    .GroupBy(
                        x => x.PropertyName,
                        (key, group) => new Dictionary<string, string[]> { { key, group.Select(x => x.ErrorMessage).Distinct().ToArray() } }
                    );
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (BlogTalksException exception)
        {
            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsync(exception.Message);
        }
    }
}