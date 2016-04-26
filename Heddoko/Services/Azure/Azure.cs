using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Services
{
    public class Azure
    {
        public static void Upload(string url, string container, Stream stream = null)
        {
            Upload(url, container, null, stream);
        }

        public static void Upload(string url, string container, string filename = null)
        {
            Upload(url, container, filename, null);
        }

        public static void Upload(string url, string container, string filename = null, Stream stream = null)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(DAL.Config.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(url);

            if (!string.IsNullOrEmpty(filename))
            {
                using (var fileStream = System.IO.File.OpenRead(filename))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
            }
            else if (stream != null)
            {
                blockBlob.UploadFromStream(stream);
            }
        }
    }
}
