using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using nitipApi.Repositroy;

namespace nitipApi.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly INitipRepository _nitipRepository;
        private readonly IProductRepository _productRepository;

        public HomeController(INitipRepository NitipRepository, IProductRepository ProductRepository)
        {
            _nitipRepository = NitipRepository;
            _productRepository = ProductRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new Dictionary<string, object>();
            result.Add("message", "Welcome to Nitip Apps API");
            return new ObjectResult(result);
        }

        [HttpGet("{id}", Name = "GetHome")]
        public IActionResult GetById(long id)
        {
            var result = new Dictionary<string, object>();
            result.Add("message", "Welcome to Nitip Apps API");
            return new ObjectResult(result);
        }

    }
}