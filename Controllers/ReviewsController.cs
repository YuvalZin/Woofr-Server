using Microsoft.AspNetCore.Mvc;
using woofr.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        // GET api/<ReviewsController>/5
        [HttpGet("{id}")]
        public ActionResult GetReviewsByProUserId(string id)
        {
            try
            {
                return Ok(Review.GetReviews(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        // POST api/<WoofsController>
        [HttpPost]
        [Route("InsertReview")]
        public ActionResult InsertReview([FromBody] Review r)
        {
            try
            {
                return Ok(r.InsertReview());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT api/<ReviewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok(Review.Delete(id));

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
