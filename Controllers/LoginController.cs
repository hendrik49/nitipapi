using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Jwt;
using nitipApi.Models;
using nitipApi.Repositroy;

namespace nitipApi.LoginControllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        // GET api/Login
        [HttpGet]
        public IActionResult Get()
        {
            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJrZXkxIjoxLCJrZXkyIjoidGhlLXZhbHVlIn0.z4nWl_itwSsz1SbxEZkxCmm9MMkIKanFvgGz_gsWIJo";
            var secret = "didok49";

            try
            {
                var data = JsonWebToken.Decode(token, secret);
                return new ObjectResult(data);
            }
            catch (SignatureVerificationException)
            {
                return new ObjectResult("Invalid Token");
            }
        }

        // GET api/Login/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Login
        [HttpPost]
        public IActionResult Post([FromBody] User item)
        {

            if (item == null)
            {
                return BadRequest();
            }
            var payload = new Dictionary<string, object>();
            var secret = "didok49";
            var data = _userRepository.Login(item);

            if (data != null)
            {
                var token = JsonWebToken.Encode(payload, secret, Jwt.JwtHashAlgorithm.HS256);
                payload.Add("token", token);
                payload.Add("status", true);
            }
            else
            {
                payload.Add("token", "username or password invalid");
                payload.Add("status", false);
            }
            return new ObjectResult(payload);
        }

        // PUT api/Login/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Login/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
