using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddress.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IActionResult GetSections() => Ok(_ProductData.GetSections());

        [HttpGet("sections/{id:int}")]
        public IActionResult GetSection(int id) => Ok(_ProductData.GetSection(id));

        [HttpGet("brands")]
        public IActionResult GetBrands() => Ok(_ProductData.GetBrands());

        [HttpGet("brands/{id:int}")]
        public IActionResult GetBrand(int id) => Ok(_ProductData.GetBrand(id));

        [HttpPost]
        public IActionResult GetProducts(ProductFilter Filter = null) => Ok(_ProductData.GetProducts(Filter));

        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id) => Ok(_ProductData.GetProductById(id));
    }
}
