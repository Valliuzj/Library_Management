using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{   
    public class ErrorController : Controller
    {
        [Route("Error/400")]
        [HttpGet]
        public IActionResult Validation(string message, string details)
        {
            // var errorModel = TempData["ErrorModel"] as ErrorModel;
            var errorModel = new ErrorModel
            {
                StatusCode = 400,
                Message = message,
                Details = details
            };
            return View("Error", errorModel);
        }

        [Route("Error/404")]
        [HttpGet]
        public IActionResult NotFound(string message, string details)
        {
            var errorModel = new ErrorModel
            {
                StatusCode = 404,
                Message = message,
                Details = details
            };
            return View("Error", errorModel);
        }
        }
}
