using System;
using System.Collections.Generic;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProcessedFrameRepository : IMongoDbRepository<ProcessedFrame>
    {
        Task<double> GetUserScoreAsync(int userID);
        Task<double> GetMultiUserScoreAsync(int[] userIDs);
        Task<double> GetTotalErgoScoreAsync();
        double GetUserScore(int userID);
        double GetMultiUserScore(int[] userIDs);
        double GetTotalErgoScore();
    }
}
