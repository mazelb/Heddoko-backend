using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace DAL.Models
{
    [DelimitedRecord(",")]
    public class ProcessedFrame
    {

        public long TimeStamp;
        public int UserID;

        [FieldConverter(ConverterKind.Double, ".")]
        public double Longitude;
        [FieldConverter(ConverterKind.Double, ".")]
        public double Latitute;

        [FieldConverter(ConverterKind.Double, ".")]
        public double TrunkBendRL;
        [FieldConverter(ConverterKind.Double, ".")]
        public double TrunkBendFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double TrunkRotRL;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RShoulderFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LShoulderFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RShoulderABD;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LShoulderABD;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RHipFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LHipFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RHipABD;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LHipABD;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RHipIntExtRot;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LHipIntExtRot;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RElbowFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LElbowFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double RKneeFE;
        [FieldConverter(ConverterKind.Double, ".")]
        public double LKneeFE;

        [FieldConverter(ConverterKind.Double, ".")]
        public double ErgoScore;
    }
}
