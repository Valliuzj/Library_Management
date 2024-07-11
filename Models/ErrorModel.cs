using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Models;

public class ErrorModel : PageModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel()
    {
    }
    
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public ErrorModel(int statusCode, string message, string details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
   
    public void OnGet(int statusCode)
    {
        StatusCode = statusCode;
        Message = "An error occurred while processing your request.";
        Details = "Detailed information can be displayed here.";
        _logger.LogError($"Error {statusCode}: {Message}");
    }
}