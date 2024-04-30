using Microsoft.AspNetCore.Mvc;
using woofr.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController : ControllerBase
    {
        // GET: api/<VetsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<VetsController>
        [HttpPost]
        [Route("GetVerifiedVets")]
        public ActionResult GetVerifiedVets([FromBody] Vet v)
        {
            try
            {
                return Ok(v.GetVets());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // GET api/<VetsController>
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Ok(Vet.GetVetById(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        
        // POST api/<VetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
       
        // POST api/<VetsController>
        [HttpPost]
        [Route("RegisterVet")]
        public ActionResult RegisterVet([FromBody] Vet v)
        {
            try
            {
                return Ok(v.RegisterVet());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT api/<VetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
