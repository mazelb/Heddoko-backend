using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Models;

namespace Heddoko.Models
{
    public class ErgoScoreViewModel : BaseViewModel
    {
        public Organization UserOrganization { get; set; }
        public Team UserTeam { get; set; }
    }
}