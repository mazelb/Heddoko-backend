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

        private IAggregateFluent<BsonDocument> GetRecordScoreAggregate(int recordId)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("RecordId", recordId))
                .Group(new BsonDocument { { "_id", "RecordId" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetHourlyScoreAggregate(int recordId)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("RecordId", recordId))
                .Group(new BsonDocument { { "_id", "$Hour" }, { "ErgoScore", new BsonDocument("$avg", "$ErgoScore") } })
                .Sort(new BsonDocument("id", -1));
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetErgoScoreRecordAggregate(int recordId)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("RecordId", recordId))
                .Group(new BsonDocument {   { "_id", "$RecordId" }, { "RecordScore", new BsonDocument("$avg", "$ErgoScore") },
                                            { "NumFrames", new BsonDocument("$sum", 1)}, { "UserId", "$UserID" },
                                            { "StartTime", new BsonDocument("$min", "$TimeStamp") },
                                            { "HourlyScores", new BsonDocument( "$push" ,
                                                    new BsonDocument { { "Hour", "$Hour" },
                                                        { "Score", new BsonDocument("$avg", "$ErgoScore") },
                                                        { "NumFrames", new BsonDocument("$sum", 1) }
                                                    }
                                                )
                                            }
                                        });
            return aggregate;
        }

        public double GetRecordScore(int recordId)
        {
            var aggregate = GetRecordScoreAggregate(recordId);

            var results = aggregate.FirstOrDefault();
            if(results != null)
            {
                ErgoScore score = BsonSerializer.Deserialize<ErgoScore>(results);
                return score.Score;
            }
            return 0;
        }

        public async Task<double> GetRecordScoreAsync(int recordId)
        {
            var aggregate = GetRecordScoreAggregate(recordId);

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                ErgoScore score = BsonSerializer.Deserialize<ErgoScore>(results);
                return score.Score;
            }
            return 0;
        }
        
        public List<HourlyScore> GetHourlyScores(int recordId)
        {
            List<HourlyScore> scores = new List<HourlyScore>();
            var aggregate = GetHourlyScoreAggregate(recordId);

            var results = aggregate.ToBsonDocument();
            if(results != null)
            {
                scores.AddRange(BsonSerializer.Deserialize<List<HourlyScore>>(results));
            }

            return scores;
        } 

        public async Task<List<HourlyScore>> GetHourlyScoresAsync(int recordId)
        {
            List<HourlyScore> scores = new List<HourlyScore>();
            var aggregate = GetHourlyScoreAggregate(recordId);

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

        public ErgoScoreRecord GetErgoScoreRecord(int recordId)
        {
            var aggregate = GetErgoScoreRecordAggregate(recordId);

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                ErgoScoreRecord record = BsonSerializer.Deserialize<ErgoScoreRecord>(results);
            }

            return null;
        }

        public async Task<ErgoScoreRecord> GetErgoScoreRecordAsync(int recordId)
        {
            var aggregate = GetErgoScoreRecordAggregate(recordId);

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                ErgoScoreRecord record = BsonSerializer.Deserialize<ErgoScoreRecord>(results);
            }

            return null;
        }
    }
}
