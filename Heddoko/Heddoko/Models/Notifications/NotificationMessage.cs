using DAL.Models.Enum;

namespace Heddoko.Models.Notifications
{
    public class NotificationMessage
    {
        public UserEventType Type { get; set; }

        public string Message { get; set; }
    }
}