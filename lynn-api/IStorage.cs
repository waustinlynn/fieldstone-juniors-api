using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lynn_api
{
    public interface IStorage
    {
        T GetDocument<T>(string container, string fileName);
        void SaveDocument<T>(T data, string container, string fileName);
        void DeleteDocument(string container, string fileName);
    }
}
