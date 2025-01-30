using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class ProductsController : ControllerBase
{
 private readonly IProductService _service;

 public ProductsController(IProductService service)
 {
  _service = service;
 }

 //v1
 [HttpGet]
 [MapToApiVersion("1.0")]
 public IActionResult GetAllProducts()
 {
  var products = _service.GetAllProducts();
  return Ok(products);
 }
//v1
 [HttpGet("{id:int}")]
 [MapToApiVersion("1.0")]
 public IActionResult GetProductById(int id)
 {
  var product = _service.GetProductById(id);
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
  _service.UpdateDescription(id,desc);
  return Ok();
 }
 
}