using System;
using System.Collections.Generic;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL
{
    public interface IProcessedFrameRepository : IMongoDbRepository<ProcessedFrame>
    {
        Task<double> GetErgoScoreOfUser(int UserID);
        Task<double> GetErgoScoreMultiUser(int[] userIDs);
    }
}
