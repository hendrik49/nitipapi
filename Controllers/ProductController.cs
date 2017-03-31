using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using nitipApi.Models;
using nitipApi.Repositroy;
using Newtonsoft.Json.Linq;
using System;

namespace nitipApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _nitipRepository;

        public ProductController(IProductRepository ProductRepository)
        {
            _nitipRepository = ProductRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var request = Request;
            var result = new Dictionary<string, object>();
            string data = string.Empty;

            try
            {
                data = Helper.Token.jwtData(request);

                if (data.Contains("id"))
                {
                    JToken token = JObject.Parse(data);
                    var datanya = _nitipRepository.GetAll();
                    result = Helper.Return.TrueReturn("data", datanya);
                }
                else
                {
                    result = Helper.Return.FalseReturn("message", "invalid user");
                }
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                result = Helper.Return.FalseReturn("message", ex.Message.ToString());
                return new ObjectResult(result);
            }

        }

        [HttpGet("{id}", Name = "GetProduct")]
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
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _nitipRepository.Add(item);

            return CreatedAtRoute("GetProduct", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Product item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var Product = _nitipRepository.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            Product.Name = item.Name;

            _nitipRepository.Update(Product);
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