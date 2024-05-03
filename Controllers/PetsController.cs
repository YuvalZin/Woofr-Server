using Microsoft.AspNetCore.Mvc;
using woofr.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        // GET api/<PetsController>/5
        [HttpGet]
        [Route("GetUserPets/{userId}")]
        public ActionResult GetUserPets(string userId)
        {
            try
            {
                return Ok(Pet.GetUserPetsById(userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<PetsController>
        [HttpPost]
        [Route("InsertPet")]
        public ActionResult InsertPet([FromBody] Pet p)
        {
            try
            {
                return Ok(p.InsertPet());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok(Pet.Delete(id));

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
