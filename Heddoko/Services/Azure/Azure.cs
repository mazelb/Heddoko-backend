/**
 * @file Azure.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.IO;
using DAL;
using DAL.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Hangfire;
using System.Diagnostics;

namespace Services
{
    public static class Azure
    {
        public static void Upload(string url, string container, Stream stream = null)
        {
            Upload(url, container, null, stream);
        }

        public static void Upload(string url, string container, string filename = null, Stream stream = null)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Config.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(url);

            if (!string.IsNullOrEmpty(filename))
            {
                using (FileStream fileStream = File.OpenRead(filename))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
            }
            else if (stream != null)
            {
                blockBlob.UploadFromStream(stream);
            }
        }

        /// <summary>
        /// Downloads a file from the Azure blob
        /// </summary>
        /// <param name="url">Url of Azure block blob reference</param>
        /// <param name="path">path to the target file</param>
        private static void DownloadToFile(string url, string path)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Config.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(DAL.Config.AssetsContainer);
            
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(url);

            blockBlob.DownloadToFile(path, FileMode.OpenOrCreate);
        }

        public static void DeleteFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }      
        }

        public static void AddRecordToDatabase(int recordId)
        {
            UnitOfWork UoW = new UnitOfWork();
            Record record = UoW.RecordRepository.Get(recordId);

            foreach (Asset asset in record.Assets)
            {
                try
                {
                    string downloadPath = Utils.DownloadPath();

                    if (!Directory.Exists(downloadPath))
                    {
                        Directory.CreateDirectory(downloadPath);
                    }

                    string path = Path.Combine(downloadPath, asset.Name);
                    DownloadToFile(asset.Image, path);
                    FileParser.AddFileToDb(path, asset.Type, UoW, record.User.Id);
                    DeleteFile(path);
                }
                catch (FileNotFoundException ex)
                {
                    Trace.TraceError($"Azure.AddRecordToDatabase.FileNotFoundException ex:{ex.GetOriginalException()}");
                }
            }
        }
    }
}