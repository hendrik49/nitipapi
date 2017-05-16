using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using nitipApi.Models;
using nitipApi.Repositroy;
using Newtonsoft.Json.Linq;
using System;

namespace nitipApi.Controllers
{
    [Route("api/[controller]")]
    public class NitipController : Controller
    {
        private INitipRepository _nitipRepository;
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;

        public NitipController(INitipRepository NitipRepository, IProductRepository ProductRepository, IUserRepository userRepo)
        {
            _nitipRepository = NitipRepository;
            _productRepository = ProductRepository;
            _userRepository = userRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new Dictionary<string, object>();
            string data = string.Empty;

            try
            {
                var request = Request;
                data = Helper.Token.jwtData(request);

                if (data.Contains("id"))
                {
                    JToken token = JObject.Parse(data);
                    int id = (int)token.SelectToken("id");
                    var datanya = _nitipRepository.FindByUser(id);
                    result = Helper.Return.TrueReturn("message", datanya);
                }
                else
                {
                    result = Helper.Return.TrueReturn("message", data);
                }
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                result = Helper.Return.TrueReturn("message", ex.Message.ToString());
                return new ObjectResult(result);
            }

        }

        [HttpGet("{id}", Name = "GetNitip")]
        public IActionResult GetById(int id)
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
            var request = Request;
            string data = Helper.Token.jwtData(request);
            var user = _userRepository.Find(data);
            if (data.Contains("id") && user != null && item != null)
            {
                _nitipRepository.Add(item, user);
                var result = Helper.Return.TrueReturn("data", item);
                return new ObjectResult(result);
            }
            else
            {
                var result = Helper.Return.FalseReturn("message", data);
                result = Helper.Return.FalseReturn("data", Json(item));
                return new ObjectResult(result);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NitipItem item)
        {
            if (item == null || item.Id != id)
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
        public IActionResult Delete(int id)
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