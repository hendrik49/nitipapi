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
        private readonly INitipRepository _nitipRepository;
        private readonly IProductRepository _productRepository;

        public NitipController(INitipRepository NitipRepository, IProductRepository ProductRepository)
        {
            _nitipRepository = NitipRepository;
            _productRepository = ProductRepository;
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
            var result = new Dictionary<string, object>();
            var request = Request;
            string data = Helper.Token.jwtData(request);

            if (data.Contains("id"))
            {
                JToken token = JObject.Parse(data);
                int id = (int)token.SelectToken("id");
                item.IdUser = id;
                item.Amount = this.Amount(item.IdProduct,item.Qty);
                _nitipRepository.Add(item);
                result = Helper.Return.TrueReturn("data", item);
                return new ObjectResult(result);
            }
            else
            {
                result = Helper.Return.FalseReturn("message", "invalid user");
                return new ObjectResult(result);

            }
        }

        public decimal Amount(int IdProduct, int qty)
        {
            var product = _productRepository.Find(IdProduct);
            var amount = product.Price * qty;
            return amount;
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