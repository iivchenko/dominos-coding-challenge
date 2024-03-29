﻿using CodingChallenge.Application.Common;
using CodingChallenge.Domain.Common;
using FluentValidation;
using System.Text.Json;

namespace CodingChallenge.WebApi.Middlewares;

public sealed class ExceptionHandlingMiddleware
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
        catch (Exception e) when (e is DomainException ||
                                  e is CodingChallengeApplicationExcepton)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var error = new
            {
                ErrorCode = StatusCodes.Status400BadRequest,
                e.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
        catch (ValidationException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var error = new
            {
                ErrorCode = StatusCodes.Status400BadRequest,
                e.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
