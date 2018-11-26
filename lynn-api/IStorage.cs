using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lynn_api
{
    public interface IStorage
    {
        string GetDocument(string container, string fileName);
        void SaveDocument(string data, string container, string fileName);
        void DeleteDocument(string container, string fileName);
    }
}
