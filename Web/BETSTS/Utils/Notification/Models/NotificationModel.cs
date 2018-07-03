using BETSTS.Utils.Notification.Models.Constants;

namespace BETSTS.Utils.Notification.Models
{
    public class NotificationModel
    {
        private string _message;

        public string Title { get; set; }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                _message = _message?.Replace(@"'", @"\'")?.Replace(@"""", @"\""");
            }
        }

        public NotificationStatus Status { get; set; }

        public NotificationModel()
        {
        }

        public NotificationModel(string title, string message, NotificationStatus status)
        {
            _message = message;
            Title = title;
            Status = status;
        }
    }
}