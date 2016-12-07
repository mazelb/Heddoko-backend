using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class ErgoScore
    {
        [BsonId]
        public int? Id { get; set; }
        public double Score { get; set; }
    }
}