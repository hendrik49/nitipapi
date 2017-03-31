using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
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
