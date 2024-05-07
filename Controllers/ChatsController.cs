using Microsoft.AspNetCore.Mvc;
using woofr.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        // GET api/<ChatsController>/5
        [HttpGet]
        [Route("GetUsersChat/{userId}")]
        public ActionResult GetUsersChat(string userId)
        {
            try
            {
                return Ok(Chat.GetUsersChat(userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<ChatsController>
        [HttpPost]
        [Route("StartChat")]

        public ActionResult StartChat([FromBody] Chat chat)
        {
            try
            {
                return Ok(chat.StartChat());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<ChatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
