﻿using Microsoft.AspNetCore.Mvc;
using woofr.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WoofsController : ControllerBase
    {
        // GET: api/<WoofsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WoofsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<WoofsController>/5
        [HttpGet]
        [Route("GetUserPosts/{userId}")]
        public ActionResult GetUserPosts(string userId)
        {
            try
            {
                return Ok(Woof.GetUserPostsById(userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<WoofsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WoofsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WoofsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
