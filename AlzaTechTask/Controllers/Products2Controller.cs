using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;
/// <summary>
/// Controller for managing products with pagination support.
/// </summary>
/// <remarks>
/// This controller provides endpoints for retrieving products with pagination.
/// It supports API versioning and is accessible under the "api/v2/products" route.
/// </remarks>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class Products2Controller : ControllerBase
{
    private readonly IProductService _service;

    public Products2Controller(IProductService service)
    {
        _service = service;
    }
    //v2
    /// <summary>
    /// Retrieves a paginated list of products.
    /// </summary>
    /// <param name="page">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of products per page (default is 10).</param>
    /// <returns>
    /// A paginated list of products. Returns HTTP 200 (OK) with the list of products.
    /// </returns>
    /// <response code="200">Returns the paginated list of products.</response>
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetAllProductsPaginated(int page = 1,int pageSize=10)
    {
        var products = _service.GetAllProductsPaginated(page,pageSize);
        return Ok(products);
    }
}