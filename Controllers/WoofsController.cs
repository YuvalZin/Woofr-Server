using Microsoft.AspNetCore.Mvc;
using woofr.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WoofsController : ControllerBase
    {
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
        // GET api/<WoofsController>/5
        [HttpGet]
        [Route("GetPetPosts/{petId}")]
        public ActionResult GetPetPosts(string petId)
        {
            try
            {
                return Ok(Woof.GetPetPostsById(petId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        // GET api/<WoofsController>/5
        [HttpGet]
        [Route("GetHomePagePosts/{userId}")]
        public ActionResult GetHomePagePosts(string userId)
        {
            try
            {
                return Ok(Woof.GetHomePagePostsById(userId));
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
        // POST api/<WoofsController>
        [HttpPost]
        [Route("InsertPost")]
        public ActionResult InsertPost([FromBody]Woof w)
        {
            try
            {
                return Ok(w.InsertPost());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // POST api/<WoofsController>
        [HttpPost]
        [Route("TagPost/{pet_id}")]
        public ActionResult TagPost([FromBody]string post_id, string pet_id)
        {
            try
            {
                return Ok(Woof.TagPostPet(post_id,pet_id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // POST api/<WoofsController>
        [HttpPost]
        [Route("LikePost/{post_id}/{user_id}")]
        public ActionResult LikePost(string post_id,string user_id)
        {
            try
            {
                return Ok(Woof.LikePost(post_id,user_id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT api/<WoofsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WoofsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok(Woof.Delete(id));
                
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
