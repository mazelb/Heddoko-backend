using System;
using System.Collections.Generic;
using DAL.Models.Enum;
using DAL.Models.MongoDocuments.Notifications;
using MongoDB.Bson;

namespace DAL.Repository.Interface
{
    public interface IUserActivityRepository : IMongoDbRepository<UserEvent>
    {
        IEnumerable<UserEvent> GetUserActivity(int userId);

        IEnumerable<UserEvent> GetUnreadUserActivity(int userId, int take, int skip);

        IEnumerable<UserEvent> GetLatestUserActivity(int userId, int take, int skip);

        void Add(UserEvent userEvent);

        long Count(int userId);

        long UnreadCount(int userId);

        void Update(UserEvent userEvent);

        UserEvent GetEvent(string id);

        void UpdateMany(List<ObjectId> ids, ReadStatus readStatus, DateTime updated);
    }
}
