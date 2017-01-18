/**
 * @file RawFrameRepository.cs
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

namespace DAL
{
    public class RawFrameRepository : MongoDbRepository<RawFrame>, IRawFrameRepository
    {
        public RawFrameRepository(HDMongoContext context)
            : base(context)
        {
        }
    }
}
