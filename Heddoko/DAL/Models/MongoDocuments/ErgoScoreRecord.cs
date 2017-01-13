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
        public ObjectId Id { get; set; } //Synced to the ID of the corresponding record 
        public int UserId { get; set; }
        public int RecordId { get; set; }
        public uint StartTime { get; set; }
        public int NumFrames { get; set; }
        public double RecordScore { get; set; }
        public HourlyScore[] HourlyScores { get; set; }
    }
}
