using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;
/// <summary>
/// Controller for managing products in the system.
/// </summary>
/// <remarks>
/// This controller provides endpoints for retrieving and updating product information.
/// It supports API versioning and is accessible under the "api/v1/products" route.
/// </remarks>
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
 /// <summary>
 /// Retrieves all products.
 /// </summary>
 /// <returns>
 /// A list of all products. Returns HTTP 200 (OK) with the list of products.
 /// </returns>
 /// <response code="200">Returns the list of products.</response>
 [HttpGet]
 [MapToApiVersion("1.0")]
 public IActionResult GetAllProducts()
 {
  var products = _service.GetAllProducts();
  return Ok(products);
 }
//v1
 /// <summary>
 /// Retrieves a specific product by its unique identifier.
 /// </summary>
 /// <param name="id">The unique identifier of the product.</param>
 /// <returns>
 /// The product with the specified ID. Returns HTTP 200 (OK) if found, otherwise HTTP 404 (Not Found).
 /// </returns>
 /// <response code="200">Returns the requested product.</response>
 /// <response code="404">If the product with the specified ID is not found.</response>
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
 /// <summary>
 /// Updates the description of a specific product.
 /// </summary>
 /// <param name="id">The unique identifier of the product to update.</param>
 /// <param name="desc">The new description for the product.</param>
 /// <returns>
 /// HTTP 200 (OK) if the update is successful.
 /// </returns>
 /// <response code="200">The product description was updated successfully.</response>
 /// <response code="404">If the product with the specified ID is not found.</response>
 [HttpPut("{id:int}")]
 [MapToApiVersion("1.0")]
 public IActionResult UpdateProductDescription(int id, [FromBody] string desc)
 {
  if (id != null)
  {
   _service.UpdateDescription(id, desc);
   return Ok();
  }
  return NotFound();
 }
 
}