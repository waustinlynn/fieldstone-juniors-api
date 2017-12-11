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

        public T GetDocument<T>(string container, string fileName)
        {
            return JsonConvert.DeserializeObject<T>(
                new StreamReader(
                    //blob.OpenReadAsync().Result
                    GetBlobContainer(container)
                        .GetBlobReference(fileName)
                        .OpenReadAsync()
                        .Result
                ).ReadToEnd()
            );
        }

        public void SaveDocument<T>(T data, string container, string fileName)
        {
            GetBlobContainer(container).GetBlockBlobReference(fileName)
                .UploadFromStreamAsync(
                    new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)))
                ).Wait();
        }
    }
}
