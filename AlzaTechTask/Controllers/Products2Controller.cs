using AlzaTechTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTechTask.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
//[Route("api/products")]
public class Products2Controller : ControllerBase
{
    private readonly IProductService _service;

    public Products2Controller(IProductService service)
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