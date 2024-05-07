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
        [HttpGet]
        [Route("GetUserFollowersById/{id}")]
        public ActionResult GetUserFollowersByToken(string id)
        {
            try
            {
                User u = new();
                return Ok(u.GetUserFollowers(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
           // GET: api/<UsersController>
        [HttpGet]
        [Route("GetUserFollowingsById/{id}")]
        public ActionResult GetUserFollowingsById(string id)
        {
            try
            {
                User u = new();
                return Ok(u.GetUserFollowings(id));
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
        [Route("FollowUnfollowUser/{follower}/{followed}")]
        public ActionResult FollowUnfollowUser(string follower, string followed)
        {
            try
            {
                User u = new();
                return Ok(u.FollowUnfollowUser(follower, followed));
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

        // PUT api/<UsersController>/5
        [HttpPut("EditProfile")]
        public ActionResult EditProfile([FromBody] User userData)
        {
            try
            {
                User u = new();
                return Ok(userData.EditProfile());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        } 

        [HttpDelete("DeleteProfile/{token}")]
        public ActionResult DeleteProfile(string token)
        {
            try
            {
                User u = new();
                return Ok(u.DeleteProfile(token));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
