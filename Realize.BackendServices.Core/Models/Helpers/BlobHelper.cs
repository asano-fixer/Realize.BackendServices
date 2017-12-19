using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    static class BlobHelper
    {
        private static CloudStorageAccount account = null;
        private static CloudBlobContainer container = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="containerName"></param>
        public static void Setup(string connectionString, string containerName)
        {
            account = CloudStorageAccount.Parse(connectionString);
            var blobClient = account.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public static string Download(string blobName)
        {
            try
            {
                using (var memory = new MemoryStream())
                {
                    container.GetBlockBlobReference(blobName).DownloadToStream(memory);
                    memory.Position = 0;
                    using (var reader = new StreamReader(memory, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
            }

            return null;
        }
    }
}
