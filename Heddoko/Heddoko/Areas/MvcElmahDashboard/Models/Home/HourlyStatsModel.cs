/**
 * @file HourlyStatsModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;

namespace Heddoko.Areas.MvcElmahDashboard.Models.Home
{
    public class HourlyStatsModel
    {
        public HourlyStatsModel()
        {
            this.AppHourlyCounters = new Dictionary<string, int[]>();
        }

        public DateTime Timestamp { get; set; }

        public DateTime RangeStart { get; set; }

        public DateTime RangeEnd { get; set; }

        public int[] HourlyCounters { get; set; }

        public List<Code.ElmahErrorItem> LastHourErrors { get; set; }

        public Dictionary<string, int[]> AppHourlyCounters { get; set; }

        public int SampleLogCount { get; set; }
    }
}