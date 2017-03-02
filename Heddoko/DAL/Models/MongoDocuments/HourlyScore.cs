using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.MongoDocuments
{
    public class HourlyScore
    {
        public int Hour { get; set; }
        public double Score { get; set; }
        public int NumFrames { get; set; }
    }
}
