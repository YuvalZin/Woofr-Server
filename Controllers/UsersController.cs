﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
                string email = form["email"];
                string imageURL = form["imageURL"];
                User u = new();
                return Ok(u.UploadProfileImage(email,imageURL));
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
