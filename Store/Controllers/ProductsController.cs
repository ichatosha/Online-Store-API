using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Services.Contract;

namespace Store.Controllers
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

        [HttpGet] // [Get] BaseUrl/api/Products(Controller name)  << EndPoint
        public async Task<IActionResult> GetAllProducts()
        {
           var result = await _productService.GetAllProductsAsync();
            return Ok(result); // 200
        }



    }
}
