using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService; 
        }

        [HttpGet("getall")]
		public IActionResult Get()
		{
			var result = _productService.GetAll();
			if(result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		[HttpGet("getbyid")]
		public IActionResult Get(int id)
		{
			var result = _productService.GetById(id);
            if (result.Success)
            {
				return Ok(result.Data);
            }
			return BadRequest(result);
        }

		[HttpPost("add")]
		public IActionResult Post(Product product)
		{
			var result = _productService.Add(product);
            if (result.Success)
            {
				return Ok(result);
            }
			return BadRequest(result);
        }

		[HttpGet("getallbyid")]
		public IActionResult GetAllById(int id)
		{
			var result = _productService.GetAllByCategoryId(id);
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result);
		}
	}
}
