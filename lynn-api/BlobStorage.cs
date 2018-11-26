using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lynn_api
{
    public class BlobStorage : IStorage
    {
        private readonly AppSettings _settings;
        public BlobStorage(AppSettings settings)
        {
            _settings = settings;
        }

        private CloudBlobContainer GetBlobContainer(string containerName)
        {
            var container = new CloudStorageAccount(
                new StorageCredentials(_settings.StorageAccountName, _settings.StorageAccountKey), true)
                .CreateCloudBlobClient()
                .GetContainerReference(containerName);
            container.CreateIfNotExistsAsync().Wait();
            return container;
        }

        public void DeleteDocument(string container, string fileName)
        {
            GetBlobContainer(container)
                .GetBlockBlobReference(fileName)
                .DeleteAsync()
                .Wait();
        }

        public string GetDocument(string container, string fileName)
        {
            return new StreamReader(
                    GetBlobContainer(container)
                        .GetBlobReference(fileName)
                        .OpenReadAsync()
                        .Result
                ).ReadToEnd();
        }

        public void SaveDocument(string data, string container, string fileName)
        {
            GetBlobContainer(container).GetBlockBlobReference(fileName)
                .UploadFromStreamAsync(
                    new MemoryStream(Encoding.UTF8.GetBytes(data))
                ).Wait();
        }
    }
}
