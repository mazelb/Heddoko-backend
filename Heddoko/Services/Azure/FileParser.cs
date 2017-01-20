/**
 * @file FileParser.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
                    ProcessFile<ProcessedFrame>(filePath, UoW.ProcessedFrameRepository.AddOne);
                    break;
                case AssetType.AnalysisFrameData:
                    ProcessFile<AnalysisFrame>(filePath, UoW.AnalysisFrameRepository.AddOne);
                    break;
                case AssetType.RawFrameData:
                    // Raw Frames are different
                    ProcessRawFrames(filePath, UoW, id);
                    break;
            }
        }
 
        /// <summary>
        /// Reads a single frame fromthe proto file
        /// </summary>
        /// <param name="memStream"></param>
        /// <param name="fStream"></param>
        /// <returns> MemoryStream containing the frame </returns>
        private static MemoryStream ReadFrame(MemoryStream memStream, FileStream fStream)
        {
            byte[] headerBuffer = new byte[HeaderSize];
            int numberofByteRead = fStream.Read(headerBuffer, 0, HeaderSize);
            if (numberofByteRead == 0)
            {
                // return empty memstream
                memStream.SetLength(0);
                return memStream;
            }

            // Get Size of frame
            int frameSize = GetPayloadSize(headerBuffer);
            byte[] byteArrayBuffer = new byte[frameSize];
            
            //read frame from file
            fStream.Read(byteArrayBuffer, 0, frameSize);
            memStream.Seek(0, SeekOrigin.Begin);
            memStream.Write(byteArrayBuffer, 0, frameSize);
            memStream.Seek(0, SeekOrigin.Begin);

            return memStream;
        }

        /// <summary>
        /// Process proto files and add them to the DB
        /// !!! Cannont Process RawFrames file, for that use ProcessRawFrames
        /// </summary>
        /// <typeparam name="T"> Type of Frame </typeparam>
        /// <param name="filePath"> </param>
        /// <param name="save"> Function used to save frames in the DB </param>
        private static void ProcessFile<T>(string filePath, Func<T,Task<Result>> save)
        {
            try
            {
                using (FileStream fStream = new FileStream(filePath, FileMode.Open))
                {
                    MemoryStream memStream = new MemoryStream();
                    
                    while (fStream.CanRead && fStream.Length > 0)
                    {
                        memStream = ReadFrame(memStream, fStream);
                        if(memStream.Length != 0)
                        {
                            T item = Serializer.Deserialize<T>(memStream);
                            save(item);
                            memStream.SetLength(0);
                        }
                    }

                    memStream.Dispose();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"FileParser.ProcessFile.Exception ex: {ex.GetOriginalException()}");
            }
        }

        /// <summary>
        /// Parses protofile into RawFrames and adds them to the DB
        /// </summary>
        /// <param name="filePath"> filepath of the proto file </param>
        private static void ProcessRawFrames(string filePath, UnitOfWork UoW, int userId)
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
