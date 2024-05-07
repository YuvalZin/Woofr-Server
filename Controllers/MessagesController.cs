using Microsoft.AspNetCore.Mvc;
using woofr.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        // GET api/<MessagesController>/5
        [HttpGet]
        [Route("GetChatMessages/{chatId}/{readerId}")]
        public ActionResult GetChatMessages(string chatId, string readerId)
        {
            try
            {
                return Ok(Message.GetChatMessages(chatId,readerId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        // POST api/<MessagesController>
        [HttpPost]
        [Route("AddMessage")]
        public ActionResult AddMessage([FromBody] Message m)
        {
            try
            {
                return Ok(m.AddMessage());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT api/<MessagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
