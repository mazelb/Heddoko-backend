using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using DAL;
using DAL.Models;


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
            var engine = new FileHelperEngine<ProcessedFrame>();

            var frames = engine.ReadFile(filePath);

            foreach (ProcessedFrame frame in frames)
            {
                //For each frame, input into DB
                UoW.MongoRepository.AddOne(frame);
            }
        }
    }
}
