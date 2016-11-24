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
        public uint timeStamp;
        public List<ImuDataFrame> imuDataFrames;
        public ReportType? reportType;
        public string gpsCoordinates;
        public uint? calibrationId;
        public int userId;

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
