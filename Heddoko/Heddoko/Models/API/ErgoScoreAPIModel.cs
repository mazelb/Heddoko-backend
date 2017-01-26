/**
 * @file ErgoScoreAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ErgoScoreAPIModel
    {
        // Users Personal score
        public double Score { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}