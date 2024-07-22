namespace LibraryManagement.Models;

public class ErrorModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

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
       
}
