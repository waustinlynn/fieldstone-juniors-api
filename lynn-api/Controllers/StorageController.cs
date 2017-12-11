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
        public dynamic Get(string id)
        {
            return _storage.GetDocument<dynamic>(id, "main.json");
        }

        [HttpPost("{id}")]
        public ActionResult Post(string id, [FromBody]dynamic jsonData)
        {
            if(jsonData == null)
            {
                return BadRequest("Can not save data, no content was delivered");
            }
            if(jsonData.FileName == null || String.IsNullOrEmpty(jsonData.FileName.Value))
            {
                return BadRequest("Data must contain the property 'FileName' at the root of the object");
            }
            _storage.SaveDocument<dynamic>(jsonData, id, jsonData.FileName.Value);
            return Ok();
        }
    }
}