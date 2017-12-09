using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public T GetDocument<T>(string container, string fileName)
        {
            var storageCredentials = new StorageCredentials(_settings.StorageAccountName, _settings.StorageAccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = cloudBlobClient.GetContainerReference(container);
            var blob = blobContainer.GetBlobReference(fileName);
            var stream = blob.OpenReadAsync().Result;
            //return JsonConvert.DeserializeObject<T>()
            return Activator.CreateInstance<T>();
        }

        public void SaveDocument<T>(string container, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
