using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Route.Core.Dtos.Products;
using Store.Route.Core.Entities;
using Store.Route.Core.Helper;
using Store.Route.Core.Services.Contract;
using Store.Route.Core.Specifications.Products;

namespace Store.Route.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet] //Get  BaseURL/api/products 
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec) //endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpec);

            return Ok(result); //200   
        }

        [HttpGet("Brands")]//Get  BaseURL/api/products/Brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await  _productService.GetAllBrandsAsync();

            return Ok(result);
        }

        [HttpGet("Types")]//Get  BaseURL/api/products/Types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await  _productService.GetAllTypesAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]//Get  BaseURL/api/products/
        public async Task<IActionResult> GetProductById(int? id) 
        {
            if (id is null) return BadRequest("Invalid Id !!");

            var result = await _productService.GetProductById(id.Value);
             
            if (result is null) return NotFound($"The Product With Id : {id} not found in Db :(");

            return Ok(result);

        }


    }
}
