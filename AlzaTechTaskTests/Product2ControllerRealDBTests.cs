using AlzaTechTask.Controllers;
using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AlzaTechTaskTests;

public class Product2ControllerRealDBTests:IDisposable
{
    private readonly IProductService _service;
    private readonly Products2Controller _controller;
    private readonly ProductsContext _context;

    public Product2ControllerRealDBTests()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ProductsContext>()
            .UseMySQL(config.GetConnectionString("Database"))
            .Options;

        _context = new ProductsContext(options);
        _context.Database.EnsureCreated();
        _service = new ProductService(_context);
        _controller = new Products2Controller(_service);
        
    }
    //get all products paginated test
    [Fact]
    public void GetAllProductsPaginatedTestRealDB()
    {
        int page =1;
        int pageSize = 10;
        var result = _controller.GetAllProductsPaginated(page,pageSize);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProducts = Assert.IsType<(IEnumerable<Product>,int)>(okResult.Value);
        var products = returnedProducts.Item1;
        
        Assert.Equal(pageSize, products.Count()); 
    }
    

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}