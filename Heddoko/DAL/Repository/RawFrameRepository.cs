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
