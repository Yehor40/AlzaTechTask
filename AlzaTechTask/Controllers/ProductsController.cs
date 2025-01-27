using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
public class ProductsController(IProductService service) : ControllerBase
{
 //v1
 [HttpGet]
 [MapToApiVersion("1.0")]
 public IActionResult GetAllProducts()
 {
  var products = service.GetAllProducts();
  return Ok(products);
 }
//v1
 [HttpGet("{id:int}")]
 [MapToApiVersion("1.0")]
 public IActionResult GetProductById(int id)
 {
  var product = service.GetProductById(id);
  if (product != null)
  {
   return Ok(product);
  }

  return NotFound();
 }
//v1
 [HttpPut("{id:int}")]
 [MapToApiVersion("1.0")]
 public IActionResult UpdateProductDescription(int id, [FromBody] string desc)
 {
  service.UpdateDescription(id,desc);
  return Ok();
 }
 
}