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
        public uint TimeStamp { get; set; }
        public List<ImuDataFrame> ImuDataFrames { get; set; }
        public ReportType? ReportType { get; set; }
        public string GPSCoordinates { get; set; }
        public uint? CalibrationId { get; set; }
        public int UserId { get; set; }

        public static RawFrame toRawFrame(FullDataFrame dataFrame, int userID)
        {
            RawFrame newFrame = new RawFrame()
            {
                TimeStamp = dataFrame.timeStamp,
                ImuDataFrames = dataFrame.imuDataFrame,
                ReportType = dataFrame.reportType,
                GPSCoordinates = dataFrame.gpsCoordinates,
                CalibrationId = dataFrame.calibrationId,
                UserId = userID
            };

            return newFrame;
        }
    }
}
