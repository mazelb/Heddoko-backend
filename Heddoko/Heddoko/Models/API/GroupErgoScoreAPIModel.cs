using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace Heddoko.Models
{
    // Model for obtaining Ergoscores for teams and organisations
    public class GroupErgoScoreAPIModel
    {
        public List<ErgoScore> userScores { get; set; }
        
        public ErgoScore groupScore { get; set; }
    }
}