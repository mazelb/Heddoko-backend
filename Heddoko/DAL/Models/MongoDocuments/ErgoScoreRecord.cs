using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DAL.Models.MongoDocuments
{
    public class ErgoScoreRecord
    {
        public ObjectId Id { get; set; } //Synced to the ID is the corresponding record 
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public int NumFrames { get; set; }
        public int RecordScore { get; set; }
        public HourlyScore[] HourlyScores { get; set; }
    }
}
