using System;
using Elect.Core.ObjUtils;
using Elect.Core.XmlUtils;
using Elect.Logger.Logging;
using Elect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BETSTS.Filters.Exception
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IElectLog _electLog;

        public ApiExceptionFilterAttribute(IElectLog electLog)
        {
            _electLog = electLog;
        }

        public override void OnException(ExceptionContext context)
        {
            // Ignore cancel action
            if (context.Exception is OperationCanceledException || context.Exception is ObjectDisposedException)
            {
                context.ExceptionHandled = true;

                return;
            }

            var errorModel = ExceptionContextHelper.GetErrorModel(context, _electLog);

            // Response Result

            if (context.HttpContext.Request.Headers[HeaderKey.Accept] == ContentType.Xml ||
                context.HttpContext.Request.Headers[HeaderKey.ContentType] == ContentType.Xml)
            {
                context.Result = new ContentResult
                {
                    ContentType = ContentType.Xml,
                    Content = XmlHelper.ToXmlString(errorModel),
                    StatusCode = context.HttpContext.Response.StatusCode
                };
            }
            else
            {
                context.Result = new ContentResult
                {
                    ContentType = ContentType.Json,
                    Content = errorModel.ToJsonString(),
                    StatusCode = errorModel.HttpResponseCode
                };
            }

            context.ExceptionHandled = true;

            // Keep base Exception

            base.OnException(context);
        }
    }
}