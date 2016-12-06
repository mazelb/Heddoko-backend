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

namespace Services
{
    public static class FileParser
    {
        private const int BufferSize = 4096;
        private const int HeaderSize = 4;
        private const byte ProtoBufByte = 0x04;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        public static void AddFileToDb(string filePath, AssetType fileType, int id)
        {
            switch (fileType)
            {
                case AssetType.ProcessedFrameData:
                    AddProcessedFramesToDb(filePath);
                    break;
                case AssetType.RawFrameData:
                    AddRawFramesToDb(filePath, id);
                    break;
                case AssetType.AnalysisFrameData:
                    AddAnalysisFramesToDb(filePath);
                    break;
            }
        }

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

        private static int GetPayloadSize(byte[] header)
        {
            return BitConverter.ToInt32(header, 0);
        }
    }
}
