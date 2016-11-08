using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.MongoDocuments;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace DAL
{
    public class ProcessedFrameRepository : MongoDbRepository<ProcessedFrame>, IProcessedFrameRepository
    {
        public ProcessedFrameRepository(HDMongoContext context)
            : base(context)
        {
        }

        private IAggregateFluent<BsonDocument> GetAggregateUserScore(int userID)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", userID))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateTeamScore(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateMuiltUserScore(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } })
                .Sort(new BsonDocument( "ErgoScore", -1 ));
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateTotalScore()
        {
            // TODO - BENB - this will not scale well and should be removed, this should be processed separately and stored if we want to keep using it 
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "null" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        /// <summary>
        /// Async version of GetUserScore
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Calculated ErgoScore</returns>
        public async Task<double> GetUserScoreAsync(int userID)
        {
            var aggregate = GetAggregateUserScore(userID);

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                ErgoScoreResult score = BsonSerializer.Deserialize<ErgoScoreResult>(results);
                return score.ErgoScore;
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
                ErgoScoreTeamResult temp = BsonSerializer.Deserialize<ErgoScoreTeamResult>(results);

                return temp.ErgoScore;
            }

            return 0;
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

            var results = await aggregate.ToListAsync();
            if (results != null)
            {
                foreach (BsonDocument result in results)
                {
                    scores.Add(BsonSerializer.Deserialize<ErgoScoreResult>(result).ToErgoScore());
                }
            }

            return scores;
        }

        /// <summary>
        /// Async version of GetTotalErgoScore
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetTotalErgoScoreAsync()
        {
            var aggregate = GetAggregateTotalScore();

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                ErgoScoreTeamResult temp = BsonSerializer.Deserialize<ErgoScoreTeamResult>(results);

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
            var aggregate = GetAggregateUserScore(userID);

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScoreResult score = BsonSerializer.Deserialize<ErgoScoreResult>(results);
                return score.ErgoScore;
            }

            return 0;
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
                ErgoScoreTeamResult temp = BsonSerializer.Deserialize<ErgoScoreTeamResult>(results);

                return temp.ErgoScore;
            }

            return 0;
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

            var results = aggregate.ToList();
            if (results != null)
            {
                foreach (BsonDocument result in results)
                {
                    scores.Add(BsonSerializer.Deserialize<ErgoScoreResult>(result).ToErgoScore());
                }
            }

            return scores;
        }

        /// <summary>
        /// Calculates Total ErgoScore from all users and organizations
        /// </summary>
        /// <returns></returns>
        public double GetTotalErgoScore()
        {
            var aggregate = GetAggregateTotalScore();

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScoreTeamResult temp = BsonSerializer.Deserialize<ErgoScoreTeamResult>(results);

                return temp.ErgoScore;
            }

            return 0;
        }
    }
}
