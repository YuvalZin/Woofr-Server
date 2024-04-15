using Microsoft.AspNetCore.Mvc;
using woofr.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public string Post([FromBody] User u)
        {
            return u.RegisterUser();
        }

        [HttpPost]
        [Route("UserLogIn/{email}/{password}")]
        public ActionResult UserLogIn(string email, string password)
        {
            try
            {
                User u = new User();
                return Ok(u.LogIn(email, password));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        // PUT api/<UsersController>/5
        [HttpPut("UploadImage/{id}")]
        public bool UploadImage(int id, [FromBody] string image)
        {
            User u = new();
            return u.UploadImage(id, image);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
