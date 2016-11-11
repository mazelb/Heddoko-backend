using System;
using System.Collections.Generic;
using DAL.Models;
using System.Threading.Tasks;
using DAL.Models.MongoDocuments;

namespace DAL
{
    public interface IProcessedFrameRepository : IMongoDbRepository<ProcessedFrame>
    {
        Task<double> GetUserScoreAsync(int userID);
        Task<double> GetTeamScoreAsync(int[] userIDs);
        Task<List<ErgoScore>> GetMultipleUserScoresAsync(int[] userIDs);
        Task<double> GetTotalErgoScoreAsync();
        double GetUserScore(int userID);
        double GetTeamScore(int[] userIDs);
        List<ErgoScore> GetMultipleUserScores(int[] userIDs);
        double GetTotalErgoScore();
    }
}
