/**
 * @file UserEvent.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using DAL.Models.Enum;
using DAL.Models.Enums;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace DAL.Models.MongoDocuments.Notifications
{
    public class UserEvent
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }

        [JsonIgnore]
        public string IdString => Id.ToString();

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public UserEventType Type { get; set; }

        public int UserId { get; set; }

        public ReadStatus ReadStatus { get; set; }

        public UserEventStatus Status { get; set; }

        public object Entity { get; set; }

        public int? EntityId { get; set; }

        public string Message { get; set; }
    }
}
