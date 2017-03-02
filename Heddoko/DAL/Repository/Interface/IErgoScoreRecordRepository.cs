using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.MongoDocuments;

namespace DAL
{
    public interface IErgoScoreRecordRepository : IMongoDbRepository<ErgoScoreRecord>
    {
        Task<double> GetUserScoreAsync(int userID);
        Task<double> GetTeamScoreAsync(int[] userIDs);
        Task<List<ErgoScoreRecord>> GetTeamErgoScoreRecordsAsync(int[] userIDs);
        Task<List<ErgoScore>> GetMultipleUserScoresAsync(int[] userIDs);
        Task<List<HourlyScore>> GetHourlyScoresForUsersAsync(int[] userIds, int hourStart, int hourEnd);
        Task<List<ErgoScoreRecord>> GetFilteredErgoScoreRecordsAsync(int[] userIDs, int?[] dates = null);
        double GetUserScore(int userID);
        double GetTeamScore(int[] userIDs);
        List<ErgoScoreRecord> GetTeamErgoScoreRecords(int[] userIDs);
        List<ErgoScore> GetMultipleUserScores(int[] userIDs);
        List<HourlyScore> GetHourlyScoresForUsers(int[] userIds, int hourStart, int hourEnd);
        List<ErgoScoreRecord> GetFilteredErgoScoreRecords(int[] userIDs, int?[] dates = null);
    }
}
