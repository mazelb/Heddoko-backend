using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.MongoDocuments;
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
        /// <param name="userID"></param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetUserScoreAsync(int userID)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument( "UserID", userID ))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync().ConfigureAwait(false);
            if (results != null)
            {
                ErgoScoreResult score = BsonSerializer.Deserialize<ErgoScoreResult>(results);
                return score.ErgoScore;
            }

            return 0;
        }

        /// <summary>
        /// Async version on GetMultiUserScore
        /// </summary>
        /// <param name="userIDs">an array of the userIDs to calculate ergoscore from</param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetMultiUserScoreAsync(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match( new BsonDocument ( "UserID", new BsonDocument("$in", new BsonArray(userIDs) ) ) )
                .Group( new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync().ConfigureAwait(false);
            if (results != null)
            {
                ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

                return temp.ErgoScore;
            }

            return 0;
        }

        /// <summary>
        /// Async version of GetTotalErgoScore
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetTotalErgoScoreAsync()
        {
            // TODO - BENB - this will not scale well and should be removed, this should be processed separately and stored if we want to keep using it 
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });

            var results = await aggregate.FirstOrDefaultAsync().ConfigureAwait(false);
            if (results != null)
            {
                ErgoScoreMultiResult temp = BsonSerializer.Deserialize<ErgoScoreMultiResult>(results);

                return temp.ErgoScore;
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
            return GetUserScoreAsync(userID).Result;
        }

        /// <summary>
        /// Calculates the ergoscore of a list of users
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public double GetMultiUserScore(int[] userIDs)
        {
            return GetMultiUserScoreAsync(userIDs).Result;
        }

        /// <summary>
        /// Calculates Total ErgoScore from all users and organizations
        /// </summary>
        /// <returns></returns>
        public double GetTotalErgoScore()
        {
            return GetTotalErgoScoreAsync().Result;
        }
    }
}
