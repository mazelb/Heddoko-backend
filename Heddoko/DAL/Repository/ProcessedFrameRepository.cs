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
        /// Calculates the ergoscore of a specific user
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<double> GetErgoScoreOfUser(int UserID)
        {
            var collection = GetCollection<ProcessedFrame>();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument( "UserID", UserID ))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync();
            ErgoScoreResult Score = BsonSerializer.Deserialize<ErgoScoreResult>(results);

            return Score.ErgoScore;
        }

        public async Task<double> GetErgoScoreMultiUser(int[] userIDs)
        {
            var collection = GetCollection<ProcessedFrame>();

            BsonDocument filters = new BsonDocument();

            var aggregate = collection.Aggregate()
                .Match( new BsonDocument ( "UserID", new BsonDocument("$in", new BsonArray(userIDs) ) ) )
                .Group( new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync();
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
