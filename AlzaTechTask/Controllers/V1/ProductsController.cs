using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers.V1;

[ApiController]
[Route("api/v1/products")]

public class ProductsController : ControllerBase
{
 private readonly IProductService _service;

 public ProductsController(IProductService service)
 {
  _service = service;
 }

 [HttpGet]
 public IActionResult GetAllProducts()
 {
  var products = _service.GetAllProducts();
  return Ok(products);
 }

 [HttpGet("{id}")]
 public IActionResult GetProductById(int id)
 {
  var product = _service.GetProductById(id);
  if (product != null)
  {
   return Ok(product);
  }

  return NotFound();
 }

 [HttpPatch("{id}")]
 public IActionResult UpdateProductDescription(int id, [FromBody] string desc)
 {
  _service.UpdateDescription(id,desc);
  return NoContent();
 }
 
}