/**
 * @file ErgoScoreViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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