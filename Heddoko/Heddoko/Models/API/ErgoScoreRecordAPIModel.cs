using System;
using DAL.Models.MongoDocuments;

namespace Heddoko.Models
{
    public class ErgoScoreRecordAPIModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double RecordScore { get; set; }
        public HourlyScore[] HourlyScores { get; set; }
    }
}