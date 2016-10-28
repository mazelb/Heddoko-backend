using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace DAL
{
    public class ProcessedFrameRepository : MongoDbRepository<ProcessedFrame>, IProcessedFrameRepository
    {
        public ProcessedFrameRepository(HDMongoContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Async version of GetUserScore
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetUserScoreAsync(int UserID)
        {
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument( "UserID", UserID ))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync();
            ErgoScoreResult Score = BsonSerializer.Deserialize<ErgoScoreResult>(results);

            return Score.ErgoScore;
        }

        /// <summary>
        /// Async version on GetMultiUserScore
        /// </summary>
        /// <param name="userIDs">an array of the userIDs to calculate ergoscore from</param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetMultiUserScoreAsync(int[] userIDs)
        {
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Match( new BsonDocument ( "UserID", new BsonDocument("$in", new BsonArray(userIDs) ) ) )
                .Group( new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync();
            ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

            return temp.ErgoScore;
        }

        /// <summary>
        /// Async version of GetTotalErgoScore
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetTotalErgoScoreAsync()
        {
            // TODO - BENB - this will not scale well and should be removed, this should be processed separately and stored if we want to keep using it 
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync();
            ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

            return temp.ErgoScore;
        }
        /// <summary>
        /// Calculates the ergoscore of a specific user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public double GetUserScore(int userID)
        {
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", userID))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScoreResult Score = BsonSerializer.Deserialize<ErgoScoreResult>(results);
                return Score.ErgoScore;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculates the ergoscore of a list of users
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public double GetMultiUserScore(int[] userIDs)
        {
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = aggregate.FirstOrDefault();
            ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

            return temp.ErgoScore;
        }

        /// <summary>
        /// Calculates Total ErgoScore from all users and organizations
        /// </summary>
        /// <returns></returns>
        public double GetTotalErgoScore()
        {
            // TODO - BENB - this will not scale well and should be removed, this should be processed separately and stored if we want to keep using it 
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = aggregate.FirstOrDefault();
            ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

            return temp.ErgoScore;
        }

    }

    public class ErgoScoreResult
    {
        public int _id { get; set; }
        public double ErgoScore { get; set; }
    }

    public class ErgoScoreMultiResult
    {
        public string _id { get; set; }
        public double ErgoScore { get; set; }
    }
}
