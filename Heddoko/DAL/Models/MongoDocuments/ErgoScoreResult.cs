﻿using DAL.Models;

namespace DAL.Models.MongoDocuments
{
    public class ErgoScoreResult
    {
        public int _id { get; set; }
        public double ErgoScore { get; set; }

        public ErgoScore ToErgoScore()
        {
            return new ErgoScore
            {
                Id = _id,
                Score = ErgoScore
            };
        }
    }
}
