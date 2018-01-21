﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lynn_api.Controllers
{
    [Produces("application/json")]
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

        [HttpGet]
        [Route("api/storage/get")]
        public ActionResult Get(string container, string id)
        {
            try
            {
                return Ok(_storage.GetDocument<dynamic>(container, id));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/storage")]
        public ActionResult Post(string container, string id, [FromBody]dynamic jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Can not save data, no content was delivered");
                }
                _storage.SaveDocument<dynamic>(jsonData, container, id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/storage")]
        public ActionResult Delete(string container, string id)
        {
            try
            {
                _storage.DeleteDocument(container, id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}