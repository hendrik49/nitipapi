using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Jwt;
using nitipApi.Models;
using nitipApi.Repositroy;

namespace nitipApi.AuthControllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        // GET api/Auth
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

        // GET api/Auth/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Auth
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
        // POST api/Auth
        [HttpPost("register")]
        public IActionResult Register([FromBody] User item)
        {
            var result = new Dictionary<string, object>();

            if (item == null)
            {
                return BadRequest();
            }

            _userRepository.Add(item)
            ;
            result.Add("status", true);
            result.Add("data", item);

            return new ObjectResult(result);
        }


        // PUT api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Auth/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
