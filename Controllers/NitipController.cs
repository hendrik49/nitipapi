using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using nitipApi.Models;
using nitipApi.Repositroy;
using Jwt;
using System.Linq;

namespace nitipApi.Controllers
{
    [Route("api/[controller]")]
    public class NitipController : Controller
    {
        private readonly INitipRepository _nitipRepository;

        public NitipController(INitipRepository NitipRepository)
        {
            _nitipRepository = NitipRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var request = Request;
            var secret = "didok49";
            string token = "token";
            var result = new Dictionary<string, object>();

            if (!request.Headers.ContainsKey("Authorization"))
            {
                result.Add("message","missing token");
                result.Add("status",true);
                return new ObjectResult(result);
            }
            else
            {
                token = request.Headers["Authorization"];
            }

            try
            {
                var data = JsonWebToken.Decode(token, secret);
                var datanya = _nitipRepository.GetAll();
                result.Add("data", datanya);
                result.Add("status",true);
                return new ObjectResult(result);
            }
            catch (SignatureVerificationException)
            {
                result.Add("message","invalid token");
                result.Add("status",true);
                return new ObjectResult(result);
            }

        }

        [HttpGet("{id}", Name = "GetNitip")]
        public IActionResult GetById(long id)
        {
            var item = _nitipRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] NitipItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _nitipRepository.Add(item);

            return CreatedAtRoute("GetNitip", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] NitipItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var Nitip = _nitipRepository.Find(id);
            if (Nitip == null)
            {
                return NotFound();
            }

            Nitip.IsComplete = item.IsComplete;
            Nitip.Name = item.Name;

            _nitipRepository.Update(Nitip);
            return new ObjectResult("success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _nitipRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _nitipRepository.Remove(id);
            return new ObjectResult("succes");
        }
    }
}