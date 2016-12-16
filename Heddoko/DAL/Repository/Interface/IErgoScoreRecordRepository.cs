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
        Task<List<ErgoScore>> GetMultipleUserScoresAsync(int[] userIDs);
        double GetUserScore(int userID);
        double GetTeamScore(int[] userIDs);
        List<ErgoScore> GetMultipleUserScores(int[] userIDs);
    }
}
