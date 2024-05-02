using Microsoft.AspNetCore.Mvc;
using woofr.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace woofr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalsController : ControllerBase
    {
        // GET: api/<ProfessionalsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<ProfessionalsController>
        [HttpPost]
        [Route("GetVerifiedProfessionals")]
        public ActionResult GetVerifiedProfessionals([FromBody] Professional p)
        {
            try
            {
                return Ok(p.GetProfessionals());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // GET: api/<ProfessionalsController>
        [HttpGet]
        [Route("GetProffesionalsForHomePage")]
        public ActionResult GetProffesionalsForHomePage()
        {
            Professional p = new Professional();
            try
            {
                return Ok(p.GetProfessionals());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        // GET api/<ProfessionalsController>
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Ok(Professional.GetProfessionalById(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        
        // POST api/<ProfessionalsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
       
        // POST api/<ProfessionalsController>
        [HttpPost]
        [Route("RegisterProfessional")]
        public ActionResult RegisterProfessional([FromBody] Professional v)
        {
            try
            {
                return Ok(v.RegisterProfessional());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        // PUT api/<ProfessionalsController>/5
        [HttpPut("UpdateProffesional")]
        public ActionResult ToUpdateProffesional([FromBody] Professional P)
        {

            try
            {
                return Ok(P.UpdateProfessional());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);

            }
        }

            // DELETE api/<ProfessionalsController>/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
            }
        }
    }

