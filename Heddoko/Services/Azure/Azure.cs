using System.IO;
using DAL;
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

        public static void Download(string url, string container, string path)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Config.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(url);

            blockBlob.DownloadToFile(path, FileMode.OpenOrCreate);
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}