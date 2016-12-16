using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.MongoDocuments;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace DAL.Repository
{
    public class ErgoScoreRecordRepository : MongoDbRepository<ErgoScoreRecord>, IErgoScoreRecordRepository
    {
        public ErgoScoreRecordRepository(HDMongoContext context)
            : base(context)
        {
        }

        private IAggregateFluent<BsonDocument> GetAggregateUserScore(int userId)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserId", userId))
                .Project(new BsonDocument { { "UserId", 1 }, { "CalcScore", new BsonDocument("$multiply", new BsonArray { "$RecordScore", "$NumFrames" }) }, { "NumFrames", 1 } })
                .Group(new BsonDocument { { "_id", "$UserId" }, { "CalcScore", new BsonDocument("$sum", "$CalcScore") }, { "NumFrames", new BsonDocument("$sum", "$NumFrames") } })
                .Project(new BsonDocument { { "_id", 1 }, { "Score", new BsonDocument("$divide", new BsonArray{"$CalcScore", "$NumFrames"})} });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateTeamScore(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Project(new BsonDocument { { "UserId", 1 }, { "CalcScore", new BsonDocument("$multiply", new BsonArray { "$RecordScore", "$NumFrames" }) }, { "NumFrames", 1 } })
                .Group(new BsonDocument { { "_id", null }, { "CalcScore", new BsonDocument("$sum", "$CalcScore") }, { "NumFrames", new BsonDocument("$sum", "$NumFrames") } })
                .Project(new BsonDocument { { "Score", new BsonDocument("$divide", new BsonArray { "$CalcScore", "$NumFrames" }) } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateMuiltUserScore(int[] userIds)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIds))))
                .Project(new BsonDocument { { "UserId", 1 }, { "CalcScore", new BsonDocument("$multiply", new BsonArray { "RecordScore", "NumFrames" }) }, { "NumFrames", 1 } })
                .Group(new BsonDocument { { "_id", "$UserId" }, { "CalcScore", new BsonDocument("$sum", "$CalcScore") }, { "NumFrames", new BsonDocument("$sum", "$NumFrames") } })
                .Project(new BsonDocument { { "_id", 1 }, { "Score", new BsonDocument("$divide", new BsonArray { "$CalcScore", "$NumFrames" }) } })
                .Sort(new BsonDocument("Score", -1));
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateHourlyScores(int[] userIds, int hourStart, int hourEnd)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIds))))
                .Unwind(x => x.HourlyScores)
                .Project(new BsonDocument { { "Hour", "$HourlyScore.Hour" }, { "CalcScore", new BsonDocument("$multiply", new BsonArray { "$HourlyScore.Score", "$HourlyScore.NumFrames" }) }, { "NumFrames", "$HourlyScore.NumFrames" } })
                .Group(new BsonDocument { { "_id", "$Hour" }, { "CalcScore", new BsonDocument("$sum", "$CalcScore") }, { "NumFrames", new BsonDocument("$sum", "$NumFrames") } })
                .Match(new BsonDocument("Hour", new BsonDocument { { "$gte", hourStart }, { "$lte", hourEnd } }))
                .Sort(new BsonDocument("Hour", 1));
            return aggregate;
        }

        /// <summary>
        /// Caluculates ErgoScores for a list of users
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns>List of UserIDs and ErgoScore</returns>
        public List<ErgoScore> GetMultipleUserScores(int[] userIDs)
        {
            List<ErgoScore> scores = new List<ErgoScore>();

            var aggregate = GetAggregateMuiltUserScore(userIDs);

            var results = aggregate.ToBsonDocument();
            if (results != null)
            {
                scores.AddRange(BsonSerializer.Deserialize<List<ErgoScore>>(results));
            }

            return scores;
        }

        /// <summary>
        /// Async version of GetMulipleUserScores
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns>List of UserIDs and ErgoScores</returns>
        public async Task<List<ErgoScore>> GetMultipleUserScoresAsync(int[] userIDs)
        {
            List<ErgoScore> scores = new List<ErgoScore>();

            var aggregate = GetAggregateMuiltUserScore(userIDs);

            var results = await aggregate.ToCursorAsync();
            if (results != null)
            {
                await results.ForEachAsync(delegate (BsonDocument result)
                {
                    scores.Add(BsonSerializer.Deserialize<ErgoScore>(result));
                });
            }

            return scores;
        }

        /// <summary>
        /// Calculates the ergoscore of a list of users
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public double GetTeamScore(int[] userIDs)
        {
            var aggregate = GetAggregateTeamScore(userIDs);

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScore temp = BsonSerializer.Deserialize<ErgoScore>(results);

                return temp.Score;
            }

            return 0;
        }

        /// <summary>
        /// Async version of GetTeamScore
        /// </summary>
        /// <param name="userIDs">an array of the userIDs to calculate ergoscore from</param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetTeamScoreAsync(int[] userIDs)
        {
            var aggregate = GetAggregateTeamScore(userIDs);

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                ErgoScore temp = BsonSerializer.Deserialize<ErgoScore>(results);

                return temp.Score;
            }

            return 0;
        }

        /// <summary>
        /// Calculates the ergoscore of a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public double GetUserScore(int userID)
        {
            var aggregate = GetAggregateUserScore(userID);

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScore score = BsonSerializer.Deserialize<ErgoScore>(results);
                return score.Score;
            }

            return 0;
        }

        /// <summary>
        /// Async version of GetUserScore
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetUserScoreAsync(int userID)
        {
            var aggregate = GetAggregateUserScore(userID);

            var result = await aggregate.FirstOrDefaultAsync();
            if (result != null)
            {
                ErgoScore score = BsonSerializer.Deserialize<ErgoScore>(result);
                return score.Score;
            }

            return 0;
        }

        public List<HourlyScore> GetHourlyScoresForUsers(int[] userIds, int hourStart, int hourEnd)
        {
            List<HourlyScore> scores = new List<HourlyScore>();
            var aggregate = GetAggregateHourlyScores(userIds, hourStart, hourEnd);

            var results = aggregate.ToBsonDocument();
            if (results != null)
            {
                scores.AddRange(BsonSerializer.Deserialize<List<HourlyScore>>(results));
            }

            return new List<HourlyScore>();
        }

        public async Task<List<HourlyScore>> GetHourlyScoreForUsers(int[] userIds, int hourStart, int hourEnd)
        {
            List<HourlyScore> scores = new List<HourlyScore>();
            var aggregate = GetAggregateHourlyScores(userIds, hourStart, hourEnd);

            var results = await aggregate.ToCursorAsync();
            if (results != null)
            {
                await results.ForEachAsync(delegate (BsonDocument result)
                {
                    scores.Add(BsonSerializer.Deserialize<HourlyScore>(result));
                });
            }
            return scores;
        }
    }
}
