using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.MongoDocuments;
using DAL.Models;

namespace DAL
{
    public interface IAnalysisFrameRepository : IMongoDbRepository<AnalysisFrame>
    {
        double GetRecordScore(int recordID);
        Task<double> GetRecordScoreAsync(int recordID);
        List<HourlyScore> GetHourlyScores(int recordID);
        Task<List<HourlyScore>> GetHourlyScoresAsync(int recordID);
        ErgoScoreRecord GetErgoScoreRecord(int recordId);
        Task<ErgoScoreRecord> GetErgoScoreRecordAsync(int recordId);
    }
}
