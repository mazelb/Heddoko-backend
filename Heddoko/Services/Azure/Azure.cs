using System.IO;
using DAL;
using DAL.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Hangfire;

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
        /// <param name="container">Azure assests container</param>
        /// <param name="path">path to the target file</param>
        private static void DownloadToFile(int assetId, string container, string path)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Config.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

            Asset asset = new UnitOfWork().AssetRepository.Get(assetId);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(asset.Url);

            blockBlob.DownloadToFile(path, FileMode.OpenOrCreate);
        }

        public static void DeleteFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }      
        }

        public static void AddFramesToDatabase(int assetId, string container, string path)
        {
            DownloadToFile(assetId, container, path);
            FileParser.AddProcessedFramesToDb(path);

            DeleteFile(path);
        }
    }
}