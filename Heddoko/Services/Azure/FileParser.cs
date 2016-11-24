using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models.MongoDocuments;
using ProtoBuf;

namespace Services
{
    public static class FileParser
    {
        private const int BufferSize = 4096;
        private const byte ProtoBufByte = 0x04;

        /// <summary>
        /// Parses file into ProcessedFrames and inputs frames into the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        public static void AddProcessedFramesToDb(string filePath)
        {
            try
            {
                UnitOfWork UoW = new UnitOfWork();
                FileStream fStream = new FileStream(filePath, FileMode.Open);
                // Header for Frames are 4 bytes
                byte[] buffer = new byte[4];
                int frameLength;
                ProcessedFrame curFrame = new ProcessedFrame();
                while (fStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    //Use header to determine length of frame
                    frameLength = BitConverter.ToInt32(buffer, 0);
                    //Read in frame and add to DB
                    curFrame = Serializer.Deserialize<ProcessedFrame>(fStream);
                    UoW.ProcessedFrameRepository.AddOne(curFrame);
                }
            }
            catch (Exception ex)
            {
                // TODO: Benb: Fill this in
            }
        }

        /// <summary>
        /// Parses protofile into AnalysisFrames and adds them to the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        public static void AddAnalysisFramesToDb(string filePath)
        {
            try
            {
                UnitOfWork UoW = new UnitOfWork();
                FileStream fStream = new FileStream(filePath, FileMode.Open);
                // Header for Frames are 4 bytes
                byte[] buffer = new byte[4];
                int frameLength;
                AnalysisFrame curFrame = new AnalysisFrame();
                while (fStream.Read(buffer, 0, buffer.Length) > 0)
                {
                    //Use header to determine length of frame
                    frameLength = BitConverter.ToInt32(buffer, 0);
                    //Read in frame and add to DB
                    curFrame = Serializer.Deserialize<AnalysisFrame>(fStream);
                    UoW.AnalysisFrameRepository.AddOne(curFrame);
                }
            }
            catch (Exception ex)
            {
                // TODO : Benb : Handle File errors
            }
        }

        /// <summary>
        /// Parses protofile into RawFrames and adds them to the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        public static void AddRawFramesToDb(string filePath, int userId)
        {
            try
            {
                UnitOfWork UoW = new UnitOfWork();
                FileStream fStream = new FileStream(filePath, FileMode.Open);
                MemoryStream memStream = new MemoryStream();
                byte[] byteArrayBuffer = new byte[BufferSize];
                RawPacket rawPacket = new RawPacket();
                RawPacket packetCopy;
                Packet packet = new Packet();
                while (fStream.CanRead)
                {

                    int numberOfByteRead = fStream.Read(byteArrayBuffer, 0, BufferSize);
                    if (numberOfByteRead == 0)
                    {
                        break;
                    }
                    for (int vI = 0; vI < numberOfByteRead; vI++)
                    {
                        byte vByte = byteArrayBuffer[vI];
                        //the byte is 0, this means that the current array buffer has received an incomplete amount of bytes
                        PacketStatus packetStatus = rawPacket.ProcessByte(vByte);
                        if (packetStatus == PacketStatus.PacketComplete)
                        {
                            // Process packet
                            packetCopy = new RawPacket(rawPacket);
                            // Check first byte of payload
                            if (packetCopy.Payload[0] == ProtoBufByte)
                            {
                                memStream.Seek(0, SeekOrigin.Begin);
                                //Write the payload, not including the first byte
                                memStream.Write(packetCopy.Payload, 1, packetCopy.PayloadSize - 1);
                                memStream.Seek(0, SeekOrigin.Begin);
                                packet = Serializer.Deserialize<Packet>(memStream);
                                UoW.RawFrameRepository.AddOne(RawFrame.toRawFrame(packet.fullDataFrame, userId));
                                memStream.SetLength(0);
                            }
                            rawPacket.ResetPacket();
                        }
                        else if (packetStatus == PacketStatus.PacketError)
                        {
                            rawPacket.ResetPacket();
                        }
                    }

                }
                fStream.Close();
            }
            catch (Exception ex)
            {
                // TODO: BENB : Handle file errors
                Console.Write(ex.Data);
            }
        }

        public static void TestFunction()
        {
            AddRawFramesToDb(Path.Combine(Config.BaseDirectory, "Download", "Frames", "testRawFile.hsm"), 1);
        }
    }
}
