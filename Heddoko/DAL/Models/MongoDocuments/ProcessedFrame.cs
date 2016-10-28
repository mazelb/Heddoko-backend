using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using ProtoBuf;

namespace DAL.Models
{
    // Using both Protobuf and FireHelpers fileParser. Once fully switched to protobuf can remove fieldconverters
    [ProtoContract][DelimitedRecord(",")]
    public class ProcessedFrame
    {
        [ProtoMember(1)]
        public long TimeStamp;
        [ProtoMember(2)]
        public int UserID;
        [ProtoMember(3)]
        public string KitID;

        [ProtoMember(4)][FieldConverter(ConverterKind.Double, ".")]
        public double Longitude;
        [ProtoMember(5)][FieldConverter(ConverterKind.Double, ".")]
        public double Latitute;

        [ProtoMember(6)][FieldConverter(ConverterKind.Double, ".")]
        public double TrunkBendRL;
        [ProtoMember(7)][FieldConverter(ConverterKind.Double, ".")]
        public double TrunkBendFE;
        [ProtoMember(8)][FieldConverter(ConverterKind.Double, ".")]
        public double TrunkRotRL;
        [ProtoMember(9)][FieldConverter(ConverterKind.Double, ".")]
        public double RShoulderFE;
        [ProtoMember(10)][FieldConverter(ConverterKind.Double, ".")]
        public double LShoulderFE;
        [ProtoMember(11)][FieldConverter(ConverterKind.Double, ".")]
        public double RShoulderABD;
        [ProtoMember(12)][FieldConverter(ConverterKind.Double, ".")]
        public double LShoulderABD;
        [ProtoMember(13)][FieldConverter(ConverterKind.Double, ".")]
        public double RHipFE;
        [ProtoMember(14)][FieldConverter(ConverterKind.Double, ".")]
        public double LHipFE;
        [ProtoMember(15)][FieldConverter(ConverterKind.Double, ".")]
        public double RHipABD;
        [ProtoMember(16)][FieldConverter(ConverterKind.Double, ".")]
        public double LHipABD;
        [ProtoMember(17)][FieldConverter(ConverterKind.Double, ".")]
        public double RHipIntExtRot;
        [ProtoMember(18)][FieldConverter(ConverterKind.Double, ".")]
        public double LHipIntExtRot;
        [ProtoMember(19)][FieldConverter(ConverterKind.Double, ".")]
        public double RElbowFE;
        [ProtoMember(20)][FieldConverter(ConverterKind.Double, ".")]
        public double LElbowFE;
        [ProtoMember(21)][FieldConverter(ConverterKind.Double, ".")]
        public double RKneeFE;
        [ProtoMember(22)][FieldConverter(ConverterKind.Double, ".")]
        public double LKneeFE;

        [ProtoMember(23)][FieldConverter(ConverterKind.Double, ".")]
        public double ErgoScore;
    }
}
