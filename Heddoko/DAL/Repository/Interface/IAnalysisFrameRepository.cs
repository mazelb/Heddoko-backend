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
