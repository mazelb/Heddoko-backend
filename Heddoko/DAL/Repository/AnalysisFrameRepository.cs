/**
 * @file AnalysisFrameRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.MongoDocuments;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using DAL.Models;

namespace DAL
{
    public class AnalysisFrameRepository : MongoDbRepository<AnalysisFrame>, IAnalysisFrameRepository
    {
        public AnalysisFrameRepository(HDMongoContext context)
            : base(context)
        {
        }

        private IAggregateFluent<BsonDocument> GetAggregateUserScore(int userID)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", userID))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "Score", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateTeamScore(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Group(new BsonDocument { { "_id", "null" }, { "Score", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateMuiltUserScore(int[] userIDs)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("UserID", new BsonDocument("$in", new BsonArray(userIDs))))
                .Group(new BsonDocument { { "_id", "$UserID" }, { "Score", new BsonDocument("$avg", "$ErgoScore") } })
                .Sort(new BsonDocument("Score", -1));
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetAggregateTotalScore()
        {
            // TODO - BENB - this will not scale well and should be removed, this should be processed separately and stored if we want to keep using it 
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Group(new BsonDocument { { "_id", "null" }, { "Score", new BsonDocument("$avg", "$ErgoScore") } });
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

            var result = await aggregate.FirstOrDefaultAsync();
            if (result != null)
            {
                ErgoScore score = BsonSerializer.Deserialize<ErgoScore>(result);
                return score.Score;
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
        /// Async version of GetMulipleUserScores
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns>List of UserIDs and ErgoScores</returns>
        public async Task<List<ErgoScore>> GetMultipleUserScoresAsync(int[] userIDs)
        {
            var aggregate = GetAggregateMuiltUserScore(userIDs);

            return aggregate.ToList().Select(score => BsonSerializer.Deserialize<ErgoScore>(score)).ToList();
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
        /// Caluculates ErgoScores for a list of users
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns>List of UserIDs and ErgoScore</returns>
        public List<ErgoScore> GetMultipleUserScores(int[] userIDs)
        {
            var aggregate = GetAggregateMuiltUserScore(userIDs);

            return aggregate.ToList().Select(score => BsonSerializer.Deserialize<ErgoScore>(score)).ToList();
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
                ErgoScore temp = BsonSerializer.Deserialize<ErgoScore>(results);

                return temp.Score;
            }

            return 0;
        }
    }
}
