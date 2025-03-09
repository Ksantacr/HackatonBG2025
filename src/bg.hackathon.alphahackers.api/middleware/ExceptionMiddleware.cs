using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using bg.hackathon.alphahackers.application.constants;
using bg.hackathon.alphahackers.application.exceptions;
using bg.hackathon.alphahackers.domain.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace bg.hackathon.alphahackers.api.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (InternalException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, GlobalConstant.ERROR_CODE_INTERNAL,GlobalConstant.MSG_ERROR_INTERNO);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, GlobalConstant.ERROR_CODE_NOT_FOUND,GlobalConstant.MSG_ERROR_NOT_FOUND);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, GlobalConstant.ERROR_CODE_INTERNAL,GlobalConstant.MSG_ERROR_INTERNO);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string errorCode, string error_message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorResponse
            {
                Code = (int)statusCode,
                TraceId = context.TraceIdentifier,
                Message = error_message,
                Errors = new List<ErrorDetail>
                {
                    new ErrorDetail
                    {
                        Code = errorCode,
                        Message = exception.Message,
                    },
                },
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
