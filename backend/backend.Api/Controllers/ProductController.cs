using backend.factory.DTOs;
using backend.factory.ServiceManager;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductBLL _productBLL;

    public ProductController(ProductBLL productBLL)
    {
        _productBLL = productBLL;
    }

    [HttpGet("get-product")]
    public async Task<IActionResult> GetProduct()
    {
        var result = await _productBLL.GetProductsAsync();
        return Ok(result);
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromBody] ProductModelDto product)
    {
        var result = await _productBLL.AddProductAsync(product);
        return Ok(result);
    }

    [HttpPut("update-product/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductModelDto product)
    {
        var result = await _productBLL.UpdateProductAsync(id, product);
        return Ok(result);
    }

    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productBLL.DeleteProductAsync(id);
        return Ok(result);
    }
}
