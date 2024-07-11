using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Web;

namespace LibraryManagement 
{

    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;


        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomValidationException ex)
            {
                _logger.LogError($"Custom Validation Exception: {ex.Message}");
                await HandleExceptionAsync(httpContext, 400, ex.Message, ex.StackTrace);
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogError($"Resource Not Found Exception: {ex.Message}");
                await HandleExceptionAsync(httpContext, 404, ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, 6000, ex.Message, ex.StackTrace);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int statusCode, string message, string details)
        {
            context.Response.Redirect($"/Error/{statusCode}?message={HttpUtility.UrlEncode(message)}&details={HttpUtility.UrlEncode(details)}");
            return Task.CompletedTask;
        }
    }

}