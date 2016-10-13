using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using DAL;


namespace Services
{
    public static class FileParser
    {
        /// <summary>
        /// Parses file into ErgoScoreFrames and inputs frames into the DB
        /// </summary>
        /// <param name="fileName">filename of the csv file (including path)</param>
        public static void AddProcessedFramesToDb(string filePath)
        {
            UnitOfWork UoW = new UnitOfWork();
            var engine = new FileHelperEngine<ProcessedFile>();

            var frames = engine.ReadFile(filePath);

            foreach (ProcessedFile frame in frames)
            {
                //For each record, input into DB
                UoW.MongoRepository.AddOne(frame);
            }
        }
    }

    [DelimitedRecord(",")]
    public class ProcessedFile
    {
        public long TimeStamp;
        public int UserID;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Longitude;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal Latitute;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal TrunkBendRL;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal TrunkBendFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal TrunkRotRL;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RShoulderFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LShoulderFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RShoulderABD;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LShoulderABD;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RHipFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LHipFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RHipABD;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LHipABD;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RHipIntExtRot;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LHipIntExtRot;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RElbowFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LElbowFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal RKneeFE;
        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal LKneeFE;

        [FieldConverter(ConverterKind.Decimal, ".")]
        public decimal ErgoScore;
    }
}
