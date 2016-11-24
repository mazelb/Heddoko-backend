﻿using System;
using System.Collections.Generic;
using DAL.Models;
using System.Threading.Tasks;
using DAL.Models.MongoDocuments;

namespace DAL
{
    public interface IProcessedFrameRepository : IMongoDbRepository<ProcessedFrame>
    {
    }
}
