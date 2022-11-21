﻿using Actibooking.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
namespace Actibooking.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went while processing {context.Request.Path}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode= HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;

                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    break;
                case OutOfRangeException outOfRangeException:
                    statusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
                    errorDetails.ErrorType = "Out Of Range";
                    break;
                case UnauthorizedExeption unauthorizedExeption:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorDetails.ErrorType = "Bad Login or Password";
                    break;
                default:
                    break;
            }
            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode=(int)statusCode;
            return context.Response.WriteAsync(response);
        }

        public class ErrorDetails
        {
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
