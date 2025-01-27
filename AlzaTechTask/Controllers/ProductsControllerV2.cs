using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("2.0")]

public class ProductsControllerV2 : ControllerBase
{
    private readonly IProductService _service;

    public ProductsControllerV2(IProductService service)
    {
        _service = service;
    }
    //v2
    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetAllProductsPaginated(int page = 2,int pageSize=10)
    {
        var products = _service.GetAllProductsPaginated(page,pageSize);
        return Ok(products);
    }
}