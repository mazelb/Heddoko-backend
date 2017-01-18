/**
 * @file RawFrame.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace DAL.Models.MongoDocuments
{
    public class RawFrame
    {
        public uint timeStamp { get; set; }
        public List<ImuDataFrame> imuDataFrames { get; set; }
        public ReportType? reportType { get; set; }
        public string gpsCoordinates { get; set; }
        public uint? calibrationId { get; set; }
        public int userId { get; set; }

        public static RawFrame toRawFrame(FullDataFrame dataFrame, int userID)
        {
            RawFrame newFrame = new RawFrame()
            {
                timeStamp = dataFrame.timeStamp,
                imuDataFrames = dataFrame.imuDataFrame,
                reportType = dataFrame.reportType,
                gpsCoordinates = dataFrame.gpsCoordinates,
                calibrationId = dataFrame.calibrationId,
                userId = userID
            };

            return newFrame;
        }
    }
}
