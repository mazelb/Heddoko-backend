using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class ErgoScore : BaseModel
    {
        [JsonIgnore]
        public double Score { get; set; }
    }
}