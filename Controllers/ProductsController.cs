using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wooliesXTest.Models;
using wooliesXTest.Services;
namespace wooliesXTest.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;
        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }
        // GET: api/products
        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedProducts(string sortOption)
        {
            if (sortOption == null)
            {
                return NotFound();
            }
            return Ok(await _productService.SortProducts(sortOption));
        }


    }
}
 