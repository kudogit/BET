using System;
using System.Linq;
using BETSTS.Core.Exceptions;
using Elect.Core.EnvUtils;
using Elect.Logger.Logging;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BETSTS.Filters.Exception
{
    public static class ExceptionContextHelper
    {
        public static ErrorModel GetErrorModel(ExceptionContext context, IElectLog electLog)
        {
            ErrorModel errorModel;

            if (context.Exception is CoreException exception)
            {
                context.Exception = null;

                errorModel = new ErrorModel(exception.Code, exception.Message, exception.AdditionalData);

                // Log Business Logic Exception
                exception.Data.Add(nameof(errorModel.Id), errorModel.Id);
                electLog.Capture(exception, LogType.Warning, context.HttpContext);

                if (exception.AdditionalData?.Any() == true)
                {
                    errorModel.AdditionalData = exception.AdditionalData;
                }

                context.HttpContext.Response.StatusCode = errorModel.HttpResponseCode;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                errorModel = new ErrorModel(ErrorCode.UnAuthorized, "UnAuthorized Access");

                context.HttpContext.Response.StatusCode = errorModel.HttpResponseCode;
            }
            else
            {
                var message =
                    EnvHelper.IsDevelopment()
                        ? context.Exception.Message
                        : "Oops! Something went wrong, please try again later";

                errorModel = new ErrorModel(ErrorCode.Unknown, message);

                // Log Code Logic Exception 
                context.Exception.Data.Add(nameof(errorModel.Id), errorModel.Id);
                electLog.Capture(context.Exception, LogType.Error, context.HttpContext);

                if (EnvHelper.IsDevelopment())
                {
                    // Add additional data
                    errorModel.AdditionalData.Add("stackTrace", context.Exception.StackTrace);
                    errorModel.AdditionalData.Add("innerException", context.Exception.InnerException?.Message);
                    errorModel.AdditionalData.Add("note",
                        "The message is exception message and additional data such as 'stackTrace', 'internalException' and 'note' only have in [Development Environment].");
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return errorModel;
        }
    }
}