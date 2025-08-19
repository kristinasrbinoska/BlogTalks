﻿using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Exceptions.BlogTalks.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogTalks.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            _logger.LogError("---- Global ValidationException exception: {exception}", exception);

            var statusCode = StatusCodes.Status400BadRequest;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
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

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (BlogTalksException exception)
        {
            _logger.LogError("---- Global BlogTalksException exception: {exception}", exception);

            await BlogTalksExceptionResponse(context, exception);
        }
        catch (AggregateException aggEx)
        {
            // Unwrap the actual exception
            var inner = aggEx.Flatten().InnerExceptions.FirstOrDefault();

            _logger.LogError("---- Global AggregateException Flatten exception: {exception}", inner);

            await (inner is BlogTalksException customEx
                ? BlogTalksExceptionResponse(context, customEx)
                : UnknownExceptionResponse(context, inner));
        }
        catch (Exception exception)
        {
            _logger.LogError("---- Global Exception exception: {exception}", exception);

            await (exception is BlogTalksException customEx
                ? BlogTalksExceptionResponse(context, customEx)
                : UnknownExceptionResponse(context, exception));
        }
    }

    private static async Task UnknownExceptionResponse(HttpContext context, Exception? inner)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new
        {
            error = inner?.Message ?? "Unexpected error",
            type = inner?.GetType().Name ?? "Unknown"
        });
    }

    private static async Task BlogTalksExceptionResponse(HttpContext context, BlogTalksException customEx)
    {
        context.Response.StatusCode = (int)customEx.StatusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            Status = (int)customEx.StatusCode,
            Type = "Custom error",
            Title = "Custom error",
            Detail = customEx.Message,
        });
    }
}