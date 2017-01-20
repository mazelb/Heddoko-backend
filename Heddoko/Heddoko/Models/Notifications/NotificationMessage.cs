/**
 * @file NotificationMessage.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models.Enum;

namespace Heddoko.Models.Notifications
{
    public class NotificationMessage
    {
        public UserEventType Type { get; set; }

        public string Text { get; set; }
    }
}