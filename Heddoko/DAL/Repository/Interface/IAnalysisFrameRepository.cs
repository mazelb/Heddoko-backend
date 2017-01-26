/**
 * @file IAnalysisFrameRepository.cs
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
using DAL.Models;

namespace DAL
{
    public interface IAnalysisFrameRepository : IMongoDbRepository<AnalysisFrame>
    {
        double GetRecordScore(int recordID);
        Task<double> GetRecordScoreAsync(int recordID);
        ErgoScoreRecord GetErgoScoreRecord(int recordId);
        Task<ErgoScoreRecord> GetErgoScoreRecordAsync(int recordId);
    }
}
