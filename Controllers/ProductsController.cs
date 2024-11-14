using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    
   // [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _productService.GetProductsAsync());
    }

   //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }
}
