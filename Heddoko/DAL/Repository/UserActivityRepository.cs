using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Models.Enums;
using DAL.Models.MongoDocuments.Notifications;
using DAL.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Repository
{
    public class UserActivityRepository : MongoDbRepository<UserEvent>, IUserActivityRepository
    {
        public UserActivityRepository(HDMongoContext mongoDbContext) : base(mongoDbContext)
        {
        }

        public IEnumerable<UserEvent> GetUserActivity(int userId)
        {
            FilterDefinition<UserEvent> filter = Builders<UserEvent>.Filter.Eq(e => e.UserId, userId);

            return GetCollection().Find(filter).ToList();
        }

        public IEnumerable<UserEvent> GetUnreadUserActivity(int userId, int take, int skip)
        {
            var sort = Builders<UserEvent>.Sort.Descending(e => e.Created);

            var builder = Builders<UserEvent>.Filter;
            var filter = builder.Eq(e => e.UserId, userId) &
                         builder.Eq(e => e.ReadStatus, ReadStatus.Unread);

            return GetCollection().Find(filter).Sort(sort).Skip(skip).Limit(take).ToList();
        }

        public IEnumerable<UserEvent> GetLatestUserActivity(int userId, int take, int skip)
        {
            var sort = Builders<UserEvent>.Sort.Descending(e => e.Created);
            var filter = Builders<UserEvent>.Filter.Eq(e => e.UserId, userId);

            return GetCollection().Find(filter).Sort(sort).Skip(skip).Limit(take).ToList();
        }

        public void Add(UserEvent userEvent)
        {
            GetCollection().InsertOne(userEvent);
        }

        public long Count(int userId)
        {
            var filter = Builders<UserEvent>.Filter.Eq(e => e.UserId, userId);

            return GetCollection().Count(filter);
        }

        public long UnreadCount(int userId)
        {
            var builder = Builders<UserEvent>.Filter;
            var filter = builder.Eq(e => e.UserId, userId) &
                         builder.Eq(e => e.ReadStatus, ReadStatus.Unread);

            return GetCollection().Count(filter);
        }

        public void Update(UserEvent userEvent)
        {
            var filter = Builders<UserEvent>.Filter.Eq(e => e.Id, userEvent.Id);
            var update = Builders<UserEvent>.Update.Set(e => e.ReadStatus, userEvent.ReadStatus)
                                                   .Set(e => e.Status, userEvent.Status)
                                                   .Set(e => e.Updated, userEvent.Updated);


            UpdateResult res = GetCollection().UpdateOne(filter, update);

            if (!res.IsAcknowledged)
            {
                Trace.TraceError($"UserActivityRepository.Update id:{userEvent.Id}, IsAcknowledged:{res.IsAcknowledged}");
            }
            else if (res.MatchedCount < 1)
            {
                Trace.TraceError($"UserActivityRepository.Update id:{userEvent.Id}, MatchedCount:{res.MatchedCount}");
            }
        }

        public void UpdateMany(List<ObjectId> ids, ReadStatus readStatus, DateTime updated)
        {
            var filterDocument = new BsonDocument("_id", new BsonDocument("$in", new BsonArray(ids)));

            var update = Builders<UserEvent>.Update.Set(e => e.ReadStatus, readStatus)
                                                   .Set(e => e.Updated, updated);

            UpdateResult res = GetCollection().UpdateMany(filterDocument, update);

            if (!res.IsAcknowledged)
            {
                Trace.TraceError($"UserActivityRepository.UpdateMany, IsAcknowledged:{res.IsAcknowledged}");
            }
        }

        public UserEvent GetEvent(string id)
        {
            var filter = Builders<UserEvent>.Filter.Eq(e => e.Id, ObjectId.Parse(id));

            return GetCollection().Find(filter).SingleOrDefault();
        }
    }
}
