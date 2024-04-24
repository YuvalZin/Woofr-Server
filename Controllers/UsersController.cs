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

        // GET: api/<UsersController>
        [HttpGet]
        [Route("GetFollowCountByToken/{token}")]
        public ActionResult GetFollowCountByToken(string token)
        {
            try
            {
                User u = new();
                return Ok(u.GetFollowCount(token));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        // GET: api/<UsersController>
        [HttpGet]
        [Route("GetUserData/{token}")]
        public ActionResult GetUserData(string token)
        {
            try
            {
                User u = new();
                return Ok(u.GetUser(token));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        } 
        
        // GET: api/<UsersController>
        [HttpGet]
        [Route("GetUserInfo/{id}")]
        public ActionResult GetUserInfo(string id)
        {
            try
            {
                User u = new();
                return Ok(u.GetUserInfoById(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

         // GET: api/<UsersController>
        [HttpGet]
        [Route("GetLikesByPostId/{id}")]
        public ActionResult GetLikesByPostId(string id)
        {
            try
            {
                User u = new();
                return Ok(u.GetLikesByPost(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

         // GET: api/<UsersController>
        [HttpPost]
        [Route("SearchUsers")]
        public ActionResult SearchUsers([FromBody] string keyword)
        {
            try
            {
                User u = new();
                return Ok(u.SearchUsers(keyword));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] User u)
        {
            try
            {
                return Ok(u.RegisterUser());
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("UserLogIn/{email}")]
        public ActionResult UserLogIn(string email, [FromBody] string password)
        {
            try
            {
                User u = new();
                return Ok(u.LogIn(email, password));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("UploadProfileImage")] // Endpoint without imageURL in the route
        public ActionResult UploadProfileImage([FromForm] IFormCollection form)
        {
            try
            {
                string id = form["id"];
                string imageURL = form["imageURL"];
                User u = new();
                return Ok(u.UploadProfileImage(id,imageURL));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
           
        }
        //// PUT api/<UsersController>/5
        //[HttpPut("UploadImage5/{token}")]
        //public bool UploadImage5(string token, [FromBody] string image)
        //{
        //    User u = new();
        //    return u.UploadImage(token, image);
        //}

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
