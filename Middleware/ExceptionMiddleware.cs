// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ViewEngines;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.Extensions.Hosting;
// using System;
// using System.Threading.Tasks;
// using System.IO;
// using Microsoft.AspNetCore.Mvc.ViewFeatures;
// using LibraryManagement.Models;

// public class ExceptionMiddleware
// {
//     private readonly RequestDelegate _next;
//     private readonly ILogger<ExceptionMiddleware> _logger;
//     private readonly IWebHostEnvironment _env;
//     private readonly ICompositeViewEngine _viewEngine;

//     public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env, ICompositeViewEngine viewEngine)
//     {
//         _next = next;
//         _logger = logger;
//         _env = env;
//         _viewEngine = viewEngine;
//     }

//     public async Task InvokeAsync(HttpContext httpContext)
//     {
//         try
//         {
//             await _next(httpContext);
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError($"Something went wrong: {ex}");
//             await HandleExceptionAsync(httpContext, ex);
//         }
//     }

//     private async Task HandleExceptionAsync(HttpContext context, Exception exception)
//     {
//         var requestId = context.TraceIdentifier;
//         var errorViewModel = new ErrorViewModel
//         {
//             RequestId = requestId,
//             Message = "An unexpected error occurred. Please try again later.",
//             Detailed = _env.IsDevelopment() ? exception.StackTrace : null
//         };

//         var actionContext = new ActionContext(context, context.GetRouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

//         var viewResult = _viewEngine.FindView(actionContext, "Error", false);

//         if (viewResult.View == null)
//         {
//             throw new InvalidOperationException($"Couldn't find 'Error' view. Searched in '{string.Join(", ", viewResult.SearchedLocations)}'");
//         }

//         context.Response.StatusCode = 500; // You can set specific status codes based on the exception type if needed
//         context.Response.ContentType = "text/html";

//         using (var writer = new StringWriter())
//         {
//             var viewContext = new ViewContext(
//                 actionContext,
//                 viewResult.View,
//                 new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorViewModel>(
//                     new EmptyModelMetadataProvider(),
//                     new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
//                 {
//                     Model = errorViewModel
//                 },
//                 new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(context, new Microsoft.AspNetCore.Mvc.ViewFeatures.SessionStateTempDataProvider()),
//                 writer,
//                 new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions()
//             );

//             await viewResult.View.RenderAsync(viewContext);
//             await context.Response.WriteAsync(writer.ToString());
//         }
//     }
// }
