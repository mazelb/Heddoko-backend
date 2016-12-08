using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Models.MongoDocuments;
using ProtoBuf;
using System.Diagnostics;

namespace Services
{
    public static class FileParser
    {
        private const int BufferSize = 4096;
        private const int HeaderSize = 4;
        private const byte ProtoBufByte = 0x04;

        /// <summary>
        /// Adding Files to the DB
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        public static void AddFileToDb(string filePath, AssetType fileType, UnitOfWork UoW, int id)
        {
            switch (fileType)
            {
                case AssetType.ProcessedFrameData:
                    AddProcessedFramesToDb(filePath, UoW);
                    break;
                case AssetType.RawFrameData:
                    AddRawFramesToDb(filePath, UoW, id);
                    break;
                case AssetType.AnalysisFrameData:
                    AddAnalysisFramesToDb(filePath, UoW);
                    break;
            }
        }

        /// <summary>
        /// Parses file into ProcessedFrames and inputs frames into the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        private static void AddProcessedFramesToDb(string filePath, UnitOfWork UoW)
        {
            try
            {
                FileStream fStream = new FileStream(filePath, FileMode.Open);
                MemoryStream memStream = new MemoryStream();
                byte[] byteArrayBuffer = new byte[BufferSize];
                ProcessedFrame processedFrame = new ProcessedFrame();
                byte[] headerBuffer = new byte[HeaderSize];
                int frameSize;

                while (fStream.CanRead)
                {
                    int numberofByteRead = fStream.Read(headerBuffer, 0, HeaderSize);
                    if (numberofByteRead == 0)
                    {
                        break;
                    }
                    frameSize = GetPayloadSize(headerBuffer);
                    Array.Resize(ref byteArrayBuffer, frameSize);

                    //read frame from file
                    fStream.Read(byteArrayBuffer, 0, frameSize);
                    memStream.Seek(0, SeekOrigin.Begin);
                    memStream.Write(byteArrayBuffer, 0, frameSize);
                    memStream.Seek(0, SeekOrigin.Begin);
                    processedFrame = Serializer.Deserialize<ProcessedFrame>(memStream);

                    //Add to the database
                    UoW.ProcessedFrameRepository.AddOne(processedFrame);
                    memStream.SetLength(0);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"FileParser.AddProcessedFramesToDb.Exception ex: {ex.GetOriginalException()}");
            }
        }

        /// <summary>
        /// Parses protofile into AnalysisFrames and adds them to the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        private static void AddAnalysisFramesToDb(string filePath, UnitOfWork UoW)
        {
            try
            {
                FileStream fStream = new FileStream(filePath, FileMode.Open);
                MemoryStream memStream = new MemoryStream();
                byte[] byteArrayBuffer = new byte[BufferSize];
                AnalysisFrame analysisFrame = new AnalysisFrame();
                byte[] headerBuffer = new byte[HeaderSize];
                int frameSize;

                while (fStream.CanRead)
                {
                    int numberofByteRead = fStream.Read(headerBuffer, 0, HeaderSize);
                    if (numberofByteRead == 0)
                    {
                        break;
                    }
                    // Get Size of frame
                    frameSize = GetPayloadSize(headerBuffer);
                    Array.Resize(ref byteArrayBuffer, frameSize);

                    //read frame from file
                    fStream.Read(byteArrayBuffer, 0, frameSize);
                    memStream.Seek(0, SeekOrigin.Begin);
                    memStream.Write(byteArrayBuffer, 0, frameSize);
                    memStream.Seek(0, SeekOrigin.Begin);
                    analysisFrame = Serializer.Deserialize<AnalysisFrame>(memStream);

                    //Add to the database
                    UoW.AnalysisFrameRepository.AddOne(analysisFrame);
                    memStream.SetLength(0);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"FileParser.AddAnalysisFramesToDb.Exception ex: {ex.GetOriginalException()}");
            }
        }

        /// <summary>
        /// Parses protofile into RawFrames and adds them to the DB
        /// </summary>
        /// <param name="filePath">filepath of the proto file</param>
        private static void AddRawFramesToDb(string filePath, UnitOfWork UoW, int userId)
        {
            try
            {
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
                Trace.TraceError($"FileParser.AddRawFramesToDb.Exception ex: {ex.GetOriginalException()}");
            }
        }

        private static int GetPayloadSize(byte[] header)
        {
            return BitConverter.ToInt32(header, 0);
        }
    }
}
