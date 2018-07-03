using System;
using BETSTS.Utils.Notification.Models;
using BETSTS.Utils.Notification.Models.Constants;
using Elect.Core.DictionaryUtils;
using Elect.Logger.Logging;
using Elect.Web.HttpUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BETSTS.Filters.Exception
{
    public class RootExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly IElectLog _electLog;

        public RootExceptionFilterAttribute(ITempDataDictionaryFactory tempDataDictionaryFactory, IElectLog electLog)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
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

            // Ajax Case
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                context.Result = new JsonResult(errorModel, Core.Models.Constants.Formatting.JsonSerializerSettings);

                context.ExceptionHandled = true;

                // Keep base Exception
                base.OnException(context);

                return;
            }

            // MVC Page
            if (context.Exception is UnauthorizedAccessException)
            {
                // TODO Redirect to un-authorization page
                context.Result = new RedirectToActionResult("Index", "Home", false);
            }
            else
            {
#if DEBUG
                context.ExceptionHandled = true;

                // Keep base Exception
                base.OnException(context);

                return;
#else
                context.Result = new RedirectToActionResult("OopsWithParamEndpoint", "Home", new { statusCode =
 (int)errorModel.Code }, false);
#endif
            }

            // Notify
            var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);

            tempData.AddOrUpdate(Models.Constants.TempDataKey.Notify,
                new NotificationModel
                {
                    Title = "Oops, something went wrong!",
                    Message = errorModel.Message,
                    Status = NotificationStatus.Error
                });

            context.ExceptionHandled = true;

            // Keep base Exception
            base.OnException(context);
        }
    }
}