using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.MongoDocuments
{
    public partial class AnalysisFrame
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int DayOfMonth { get; set; }
        public int RecordId { get; set; }
    }
}
