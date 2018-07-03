using BETSTS.Core.Exceptions;
using BETSTS.Models.Constants;
using BETSTS.Utils.Notification.Models;
using BETSTS.Utils.Notification.Models.Constants;
using Elect.Web.ITempDataDictionaryUtils;
using Microsoft.AspNetCore.Mvc;

namespace BETSTS.Utils.Notification
{
    public static class ControllerExtensions
    {
        public static void SetNotification(this Controller controller, string title, string message, NotificationStatus status = NotificationStatus.Info)
        {
            controller.TempData.Set(TempDataKey.Notify, new NotificationModel(title, message, status));
        }

        public static void SetNotification(this Controller controller, string title, CoreException exception, NotificationStatus status = NotificationStatus.Error)
        {
            var errorCode = new ErrorModel(exception.Code, null);

            var message = errorCode.Message;

            controller.SetNotification(title, message, status);
        }

        public static void RemoveNotify(this Controller controller)
        {
            controller.TempData.Remove(TempDataKey.Notify);
        }
    }
}