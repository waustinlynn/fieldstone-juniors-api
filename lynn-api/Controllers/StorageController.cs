using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


        [HttpGet]
        [Route("api/teams")]
        public ActionResult GetTeams()
        {
            try
            {
                return Ok(_storage.GetDocument("fs-jrs", "teams.txt"));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/teams")]
        public ActionResult SaveTeams([FromBody]dynamic data)
        {
            try
            {
                if (data == null) return BadRequest("No data posted");
                _storage.SaveDocument(JsonConvert.SerializeObject(data), "fs-jrs", "teams.txt");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/data")]
        public ActionResult GetData()
        {
            try
            {
                return Ok(_storage.GetDocument("fs-jrs", "data.txt"));
            }
            catch
            {
                return BadRequest("Could not retrieve document");
            }
        }

        [HttpPost]
        [Route("api/storage")]
        public ActionResult Post([FromBody]dynamic jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Can not save data, no content was delivered");
                }

                _storage.SaveDocument(JsonConvert.SerializeObject(jsonData), "fs-jrs", "data.txt");
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