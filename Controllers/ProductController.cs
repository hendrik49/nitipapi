using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using nitipApi.Models;
using nitipApi.Repositroy;
using Newtonsoft.Json.Linq;
using System;
using nitipApi.Helper;

namespace nitipApi.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _nitipRepository;
        private readonly IUserRepository _userRepository;

        public ProductController(IProductRepository ProductRepository, IUserRepository UserRepository)
        {
            _nitipRepository = ProductRepository;
            _userRepository = UserRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var request = Request;
            var result = new Dictionary<string, object>();

            try
            {
                var user = _userRepository.jwtData(request);//jwt

                if (user != null)
                {
                    var datanya = _nitipRepository.GetAll();
                    result = Helper.Return.TrueReturn("data", datanya);
                }
                else
                {
                    result = Helper.Return.FalseReturn("data", "invalid token");
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
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var request = Request;
            var result = new Dictionary<string, object>();

            try
            {
                var user = _userRepository.jwtData(request);
                if (user != null)
                {
                    _nitipRepository.Add(item);
                    return CreatedAtRoute("GetProduct", new { id = item.Id }, item);
                }
                else
                {
                    result = Helper.Return.FalseReturn("message", "Invalid Token");
                    return new ObjectResult(result);
                }
            }
            catch (Exception ex)
            {
                result = Helper.Return.FalseReturn("message", ex.Message.ToString());
                return new ObjectResult(result);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product item)
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