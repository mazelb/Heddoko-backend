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
    }
}
