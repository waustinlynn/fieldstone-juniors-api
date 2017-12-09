using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lynn_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Storage")]
    public class StorageController : Controller
    {
        private readonly IStorage _storage;
        public StorageController(IStorage storage)
        {
            _storage = storage;
        }

        public TestModel Get()
        {
            return new TestModel() { Name = "Test Name", Prop2 = "Test Property" };
        }

        [HttpGet("{id}")]
        public TestModel Get(string id)
        {
            return new TestModel() { Name = id } ;
        }
    }
}