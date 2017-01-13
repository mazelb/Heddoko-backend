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
                .Group(new BsonDocument { { "_id", "RecordId" }, { "Score", new BsonDocument("$avg", "$ErgoScore") } });
            return aggregate;
        }

        private IAggregateFluent<BsonDocument> GetErgoScoreRecordAggregate(int recordId)
        {
            var collection = GetCollection();

            var aggregate = collection.Aggregate()
                .Match(new BsonDocument("RecordId", recordId))
                .Group(new BsonDocument {   { "_id", "$Hour" }, { "Score", new BsonDocument("$avg", "$ErgoScore") },
                                            { "NumFrames", new BsonDocument("$sum", 1)}, { "UserId", new BsonDocument("$first", "$UserId") },
                                            { "StartTime", new BsonDocument("$min", "$TimeStamp") }, { "RecordId", new BsonDocument("$first", "$RecordId")}
                                        })
                .Group(new BsonDocument {   { "_id", "$RecordId" }, { "RecordScore", new BsonDocument("$avg", "$Score") },
                                            { "NumFrames", new BsonDocument("$sum", "$NumFrames")}, { "UserId", new BsonDocument("$first", "$UserId") },
                                            { "StartTime", new BsonDocument("$min", "$StartTime") },
                                            { "HourlyScores", new BsonDocument( "$push" ,
                                                    new BsonDocument { { "Hour", "$_id" },
                                                        { "Score", "$Score" },
                                                        { "NumFrames", "$NumFrames" }
                                                    }
                                                )
                                            }
                                        })
                .Project(new BsonDocument { { "_id", 0 }, { "RecordId", "$_id" }, { "RecordScore", 1 }, { "NumFrames", 1 }, { "UserId", 1 },
                                            { "StartTime", 1 }, { "HourlyScores", 1 } } );
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

        public ErgoScoreRecord GetErgoScoreRecord(int recordId)
        {
            ErgoScoreRecord record = new ErgoScoreRecord();
            var aggregate = GetErgoScoreRecordAggregate(recordId);

            var results = aggregate.FirstOrDefault();
            if (results != null)
            {
                record = BsonSerializer.Deserialize<ErgoScoreRecord>(results);
            }

            return record;
        }

        public async Task<ErgoScoreRecord> GetErgoScoreRecordAsync(int recordId)
        {
            ErgoScoreRecord record = new ErgoScoreRecord();
            var aggregate = GetErgoScoreRecordAggregate(recordId);

            var results = await aggregate.FirstOrDefaultAsync();
            if (results != null)
            {
                record = BsonSerializer.Deserialize<ErgoScoreRecord>(results);
            }

            return record;
        }
    }
}
