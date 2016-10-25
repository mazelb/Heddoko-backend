using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ErgoScoreAPIModel
    {
        // Users Personal score
        public double UserScore { get; set; }
        // Score of the user's organization
        public double OrgScore { get; set; }
    }
}